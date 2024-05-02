using MediatR;

namespace BG.CampusLife.Application.Notifications.Commands.SendNotification
{
    public class SendNotificationCommand : IRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}