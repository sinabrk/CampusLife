using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class NotificationRepository : BaseRepository<CampusContext, NotificationRepository>, INotificationRepository
    {
        public NotificationRepository(CampusContext context, ILogger<NotificationRepository> logger, IMapper mapper) :
            base(context, logger, mapper)
        {
        }

        public async Task<Result<int>> Create(Notification notification)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            
            await Context.Notifications.AddAsync(notification);
            await Context.SaveChangesAsync();
            
            return result;
        }

        public async Task<Result<int>> BulkCreate(List<Notification> notifications)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            
            await Context.Notifications.AddRangeAsync(notifications);
            await Context.SaveChangesAsync();

            return result;
        }

        public async Task<Result<int>> SetNotificationVisited(Guid id, string userId)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            
            var entity = await Context.Notifications.FirstOrDefaultAsync(n => n.Id == id && n.UserId == Context.Users.FirstOrDefault(u => u.UserId == userId).Id);
            if (entity is null)
            {
                result.Message = $"Notification not found with {id}";
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
            }
            else
            {
                entity.Visited = true;
                await Context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Result<Notification>> GetById(Guid id, string userId)
        {
            var result = new Result<Notification>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful
            };
            var entity = await Context.Notifications.Where(n => n.Id == id && n.UserId == Context.Users.FirstOrDefault(u => u.UserId == userId).Id).Select(n =>
                    new Notification()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Body = n.Body,
                        Visited = n.Visited,
                        SendDate = n.SendDate,
                        SeenDate = n.SeenDate,
                        UserId = n.UserId,
                    })
                .FirstOrDefaultAsync();

            if (entity is null)
            {
                result.Message = $"Notification not found with {id} or {userId}";
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
            }
            else
            {
                result.Entity = entity;
            }

            return result;

        }

        public async Task<Result<Notification>> GetUserNotifications(string userId)
        {
            var entities = await Context.Notifications.Where(n => n.UserId == Context.Users.FirstOrDefault(u => u.UserId == userId).Id).Select(n => new Notification()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Body = n.Body,
                    Visited = n.Visited,
                    SendDate = n.SendDate,
                    SeenDate = n.SeenDate,
                    UserId = n.UserId,
                })
                .ToListAsync();

            return new Result<Notification>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count
            };
        }
    }
}