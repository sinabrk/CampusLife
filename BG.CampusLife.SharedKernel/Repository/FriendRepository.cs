using AutoMapper;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class FriendRepository : BaseRepository<CampusContext, FriendRepository>, IFriendRepository
    {
        #region Ctor

        public FriendRepository(CampusContext context, ILogger<FriendRepository> logger, IMapper mapper) : base(
            context, logger, mapper)
        {
        }

        #endregion

        #region Commands

        public async Task<Result<int>> AddNewFriend(User user, Guid friendId)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };

            if (!await Context.Users.AnyAsync(c => c.Id == friendId))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"User not found with {friendId}";
                return result;
            }

            Friend firstPerson = new()
            {
                UserId = user.Id,
                FriendId = friendId,
            };

            Friend secondPerson = new()
            {
                UserId = friendId,
                FriendId = user.Id
            };

            Context.Friends.AddRange(firstPerson, secondPerson);
            await Context.SaveChangesAsync();
            return result;
        }

        // public async Task BlockFriend(User user, string blockFriendId)
        // {
        //     if(!await IsFriendshipExists(blockFriendId))
        //         throw new NotFoundException($"There is no existing friend with the following id: { blockFriendId }.");
        //
        //     var connection = Context.Friends.Where(x => x.FriendId == blockFriendId && x.UserId == userId).FirstOrDefault() ?? throw new NotFoundException("FriendId", blockFriendId);
        //     connection.IsBlocked = true;
        //
        //     Context.Entry(connection).State = EntityState.Modified;
        //     await Context.SaveChangesAsync();
        // }

        public async Task<Result<int>> DeleteFriend(User userEntity, Guid friendId)
        {
            
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            
            if (!await Context.Users.AnyAsync(c => c.Id == friendId))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"User not found with {friendId}";
                return result;
            }
            
            if (!await IsFriendshipExists(friendId))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"There is no existing friend with the following id: {friendId}.";
                return result;
            }

            var friend = await Context.Friends.Where(x => x.FriendId == friendId && x.UserId == userEntity.Id)
                .FirstOrDefaultAsync();
            var user = await Context.Friends.Where(x => x.UserId == friendId && x.FriendId == userEntity.Id)
                .FirstOrDefaultAsync();

            Context.Friends.RemoveRange(user, friend);
            await Context.SaveChangesAsync();
            
            return result;
        }

        #endregion

        #region Queries

        public async Task<Result<User>> GetAllFriends(User user)
        {

            var result = new Result<User>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = await Context.Friends
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new User
                    {
                        FirstName = x.UserNavigation.FirstName,
                        LastName = x.UserNavigation.LastName,
                        Email = x.FriendNavigation.Email
                    })
                    .ToListAsync()
            };

            result.Total = result.Entities.Count;
            return result;
        }

        public async Task<Result<User>> GetFriendById(User user, Guid friendId)
        {
            var result = new Result<User>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            if (!await IsFriendshipExists(friendId))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"There is no existing friend with the following id: {friendId}.";
                return result;
            }
            
            result.Entity = await Context.Friends
                .Where(x => x.FriendId == friendId)
                .Select(x => new User()
                {
                    Id = x.UserNavigation.Id,
                    FirstName = x.UserNavigation.FirstName,
                    LastName = x.UserNavigation.LastName,
                    Email = x.FriendNavigation.Email
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Result<User>> GetFriendByName(User user, string firstName, string lastName)
        {
            var result = new Result<User>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = await Context.Friends
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new User()
                    {
                        Id = x.FriendId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = x.FriendNavigation.Email
                    })
                    .ToListAsync()
            };

            result.Total = result.Entities.Count;

            return result;
        }

        public async Task<Result<User>> GetFriendByEmail(User user, string email)
        {
            var result = new Result<User>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Friends
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new User()
                    {
                        Id = x.FriendId,
                        Email = email,
                        FirstName = x.FriendNavigation.FirstName,
                        LastName = x.FriendNavigation.LastName
                    })
                    .FirstOrDefaultAsync()
            };

            return result;
        }

        // public async Task<List<User>> GetAllBlockedFriends(User user)
        // {
        //     if (!await IsUserExists(userId))
        //         throw new NotFoundException($"User with the id { userId } does not exists"); 
        //
        //     return await Context.Friends
        //         .Where(x => x.UserId == userId && x.IsBlocked == true)
        //         .Select(x => new User()
        //         {
        //             Id = x.FriendId,
        //             Email = x.FriendNavigation.Email,
        //             FirstName = x.FriendNavigation.FirstName,
        //             LastName = x.FriendNavigation.LastName
        //         })
        //         .ToListAsync();
        // }

        #endregion

        #region Validation

        private async Task<bool> IsFriendshipExists(Guid friendId) =>
            await Context.Friends.AnyAsync(x => x.FriendId == friendId);

        // private async Task<bool> IsUserExists(string id) =>
            // await Context.Users.AnyAsync(x => x.UserId == id);


        #endregion
    }
}