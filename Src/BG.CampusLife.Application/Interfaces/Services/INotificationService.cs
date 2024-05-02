namespace BG.CampusLife.Application.Interfaces.Services;

public interface INotificationService
{
    /// <summary>
    /// Send Notification to All of users
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendNotificationToAll(object message);

    /// <summary>
    /// Send Notification to a specific user
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendNotificationToUser(string userName, object message);
}