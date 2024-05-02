using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Infrastructure.Identity
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityDbContext _identityContext;
        private readonly CampusContext _campusContext;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserManager(UserManager<ApplicationUser> userManager, IdentityDbContext identityContext,
            RoleManager<IdentityRole> roleManager, CampusContext campusContext, IEmailSender emailSender)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _campusContext = campusContext;
            _emailSender = emailSender;
        }

        public async Task<(IdentityResultHandler result, string userId)> CreateUserAsync(string email,
            string password, Roles role, Guid? locationId, Guid? universityId, byte gender, string firstName,
            string lastName)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                IsSuspend = false,
                IsActive = true,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded) return (result.ToResult(), applicationUser.Id);

            var roleEntity = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == role.ToString());

            if (roleEntity == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = role.ToString() });
                roleEntity = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == role.ToString());
            }

            await _userManager.AddToRoleAsync(applicationUser, roleEntity.Name);

            await _identityContext.SaveChangesAsync();

            var user = new User()
            {
                UserId = applicationUser.Id,
                UniversityId = universityId,
                LocationId = locationId,
                Gender = gender,
                Bio = "",
                FirstName = firstName ?? "",
                LastName = lastName ?? "",
                Email = email,
                NormalizedEmail = email.ToUpper(),
            };

            await _campusContext.Users.AddAsync(user);
            await _campusContext.SaveChangesAsync();

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

            Console.WriteLine($"Here is the {emailToken}");

            // Todo This Url must be in frontend routes
            var url = "http://127.0.0.1/EmailConfirmation?email=" + email + "&token=" + emailToken;

            // Todo Email Body
            //await _emailSender.SendEmailAsync(email, "Campus Life Confirmation Email", SendingEmail.CreateEmailBody(email, url, EmailTypes.Register));

            return (result.ToResult(), applicationUser.Id);
        }

        public async Task<Result<UserData>> GetUserAsync(string userId)
        {
            var result = new Result<UserData>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _identityContext.Users.Where(u => u.Id == userId).Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsSuspend = u.IsSuspend,
                IsActive = u.IsActive,
                ConnectionId = u.ConnectionId,
            }).FirstOrDefaultAsync();
            
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            result.Entity = new UserData()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Suspended = user.IsSuspend,
                IsActive = user.IsActive,
                ConnectionId = user.ConnectionId,
            };
            
            return result;
        }

        public async Task<Result<string>> GetUserRolesAsync(string userId)
        {
            var result = new Result<string>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            result.Entities = (List<string>)await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<Result<int>> AddRolesToUser(string userId, IEnumerable<string> roles)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

        public async Task<Result<int>> RemoveRolesFromUser(string userId, List<string> roles)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            return result;
        }

        public async Task<Result<int>> ChangePassword(string userId, string oldPass, string newPass)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }
            
            await _userManager.ChangePasswordAsync(user, oldPass, newPass);
            
            return result;
        }

        public async Task<Result<int>> ResetPasswordToken(string userName)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userName}";
                return result;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Todo Send Reset Password Email

            return result;
        }

        public async Task<Result<int>> ResetPassword(string userName, string newPassword)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userName}";
                return result;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            return result;
        }

        public async Task<Result<int>> ResetPassword(string userName, string newPassword, string token)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userName}";
                return result;
            }

            await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        public async Task<IdentityResultHandler> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByNameAsync(email);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.ToResult();
        }

        public async Task<Result<UserData>> GetUsers(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            var entities = users.Select(u => new UserData()
            {
                UserName = u.UserName,
                Email = u.Email,
                Id = u.Id,
                Suspended = u.IsSuspend,
                IsActive = u.IsActive,
                ConnectionId = u.ConnectionId,
            }).ToList();
            
            return new Result<UserData>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<string>> GetRoles()
        {
            var entities = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return new Result<string>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<UserData>> GetUserByEmailAsync(string email)
        {
            var result = new Result<UserData>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _identityContext.Users.Where(u => u.Email == email).Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsSuspend = u.IsSuspend,
                IsActive = u.IsActive,
                ConnectionId = u.ConnectionId,
            }).FirstOrDefaultAsync();
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {email}";
                return result;
            }
            
            result.Entity = new UserData()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Suspended = user.IsSuspend,
                IsActive = user.IsActive,
                ConnectionId = user.ConnectionId,
            };

            return result;
        }

        public async Task<Result<int>> SuspendUser(string userId)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _identityContext.Users.Where(u => u.Id == userId).Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsSuspend = u.IsSuspend,
                IsActive = u.IsActive,
                ConnectionId = u.ConnectionId,
            }).FirstOrDefaultAsync();

            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            user.IsSuspend = !user.IsSuspend;

            await _identityContext.SaveChangesAsync();

            return result;
        }

        public async Task<Result<int>> AccountDeactivate(string userId)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };

            var user = await _identityContext.Users.Where(u => u.Id == userId).Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsSuspend = u.IsSuspend,
                IsActive = u.IsActive,
                ConnectionId = u.ConnectionId,
            }).FirstOrDefaultAsync();

            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userId}";
                return result;
            }

            if (user.LastDeactivate.AddDays(7) < DateTime.Now)
            {
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Succeeded = false;
                result.Message = "You can only Deactivate your account once every 7 days.";
                return result;
            }

            user.IsActive = false;
            user.LastDeactivate = DateTime.Now;

            await _identityContext.SaveChangesAsync();

            return result;
        }

        public async Task<Result<int>> UpdateUserConnectionId(string userName, string connectionId)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };

            var user = await _userManager.FindByNameAsync(userName);
   
            if (user is null)
            {
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
                result.Message = $"User not found with {userName}";
                return result;
            }

            user.ConnectionId = connectionId;
            await _identityContext.SaveChangesAsync();

            return result;
        }
    }
}