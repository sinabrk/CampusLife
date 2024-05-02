namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<Result<int>> CreateNotification(Notification notification, CancellationToken cancellationToken);
    Task<Result<int>> BulkCreateNotifications(List<Notification> notifications, CancellationToken cancellationToken);
    Task<Result<int>> SetNotificationVisited(Guid id, string userId, CancellationToken cancellationToken);
    Task<Result<Notification>> GetNotificationById(Guid id, string userId);
    Task<Result<Notification>> GetUserNotifications(string userId);
}