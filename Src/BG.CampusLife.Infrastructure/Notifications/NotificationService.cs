namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : INotificationService
{
    public async Task SendNotificationToAll(object message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
    }

    public async Task SendNotificationToUser(string userName, object message)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            await _hubContext.Clients.Group(userName).SendAsync("ReceiveNotification", message);
        }
    }
}