using System.Collections.Generic;
using MediatR;

namespace BG.CampusLife.Application.Notifications.Queries.GetUserNotifications
{
    public class GetUserNotificationsQuery : IRequest<List<UserNotificationDto>>
    {
        
    }
}