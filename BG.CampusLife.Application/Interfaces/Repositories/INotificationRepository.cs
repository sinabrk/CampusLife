using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Result<int>> Create(Notification notification);
        Task<Result<int>> BulkCreate(List<Notification> notifications);
        Task<Result<int>> SetNotificationVisited(Guid id, string userId);
        Task<Result<Notification>> GetById(Guid id, string userId);
        Task<Result<Notification>> GetUserNotifications(string userId);
    }
}