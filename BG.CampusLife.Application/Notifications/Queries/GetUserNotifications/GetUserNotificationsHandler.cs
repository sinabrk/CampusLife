using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Notifications.Queries.GetUserNotifications
{
    public class GetUserNotificationsHandler : BaseHandler<GetUserNotificationsHandler>, IRequestHandler<GetUserNotificationsQuery, List<UserNotificationDto>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly INotificationRepository _notificationRepository;
        public GetUserNotificationsHandler(ILogger<GetUserNotificationsHandler> logger, IMapper mapper, ICurrentUserService currentUser, INotificationRepository notificationRepository) : base(logger, mapper)
        {
            _currentUser = currentUser;
            _notificationRepository = notificationRepository;
        }

        public async Task<List<UserNotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetUserNotifications(_currentUser.UserId);
            return Mapper.Map<List<UserNotificationDto>>(notifications.Entities);
        }
    }
}