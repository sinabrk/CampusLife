using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BG.CampusLife.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {

        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

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
}