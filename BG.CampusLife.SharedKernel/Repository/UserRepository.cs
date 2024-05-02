using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class UserRepository : BaseRepository<CampusContext, UserRepository>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(CampusContext context, ILogger<UserRepository> logger, IMapper mapper, UserManager<ApplicationUser> userManager) : base(context, logger, mapper)
        {
            _userManager = userManager;
        }

        public async Task<Result<User>> GetById(string userId)
        {
            var result = new Result<User>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Users.FirstOrDefaultAsync(c => c.UserId == userId),
            };

            if (result.Entity is not null) return result;

            result.Succeeded = false;
            result.Message = $"User not found with {userId}";
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;
        }

        public async Task<Result<User>> GetByEmail(string email)
        {
            var result = new Result<User>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Users.FirstOrDefaultAsync(c => c.NormalizedEmail == email.ToUpper()),
            };

            if (result.Entity is not null) return result;

            result.Succeeded = false;
            result.Message = $"User not found with {email}";
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;

        }

        public async Task<Result<User>> UpdateProfile(User user)
        {
            var result = new Result<User>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };
            
            Context.Entry(user).State = EntityState.Modified;
            Context.Update(user);
            await Context.SaveChangesAsync();
            result.Entity = user;

            return result;
        }

        public async Task<Result<User>> GetUserProfile(User userEntity)
        {
            var result = new Result<User>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };
            
            var user = await _userManager.FindByIdAsync(userEntity.UserId);

            var userRole = await _userManager.GetRolesAsync(user);
            
            result.Entity = await Context.Users.Select(t => new User()
            {
                UniversityId = t.UniversityId,
                LocationId = t.LocationId,
                Private = t.Private,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Gender = t.Gender,
                Birthday = t.Birthday,
                Bio = t.Bio,
                NationalityId = t.NationalityId,
                MarriageStatus = t.MarriageStatus,
                HomeLocationId = t.HomeLocationId,
                Started = (userRole.Contains("Student")) ? t.Started : null,
                Graduation = (userRole.Contains("Student")) ? t.Graduation : null,
                Graduated = (userRole.Contains("Student")) ? t.Graduated : null,
                Title = (userRole.Contains("Faculty")) ? t.Title : null,
                PersonalEmail = (userRole.Contains("Faculty")) ? t.PersonalEmail : null,
                AdditionalEmail = (userRole.Contains("Explorer")) ? t.AdditionalEmail : null,
            }).FirstOrDefaultAsync(u => u.UserId == user.Id);

            return result;
        }
    }
}