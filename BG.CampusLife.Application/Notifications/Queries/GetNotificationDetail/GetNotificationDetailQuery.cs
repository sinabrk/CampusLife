using System;
using MediatR;

namespace BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail
{
    public class GetNotificationDetailQuery : IRequest<NotificationDetailDto>
    {
        public Guid Id { get; set; }
    }
}