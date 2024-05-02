using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail
{
    public class GetNotificationDetailHandler : BaseHandler<GetNotificationDetailHandler>, IRequestHandler<GetNotificationDetailQuery, NotificationDetailDto>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUser;
        public GetNotificationDetailHandler(ILogger<GetNotificationDetailHandler> logger, IMapper mapper, INotificationRepository notificationRepository, ICurrentUserService currentUser) : base(logger, mapper)
        {
            _notificationRepository = notificationRepository;
            _currentUser = currentUser;
        }

        public async Task<NotificationDetailDto> Handle(GetNotificationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _notificationRepository.GetById(request.Id, _currentUser.UserId);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Mapper.Map<NotificationDetailDto>(result.Entity);
        }
    }
}