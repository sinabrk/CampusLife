using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Notifications.Commands.SetNotificationVisited
{
    public class SetNotificationVisitedHandler : BaseHandler<SetNotificationVisitedHandler>, IRequestHandler<SetNotificationVisitedCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUser;
        public SetNotificationVisitedHandler(ILogger<SetNotificationVisitedHandler> logger, IMapper mapper, INotificationRepository notificationRepository, ICurrentUserService currentUser) : base(logger, mapper)
        {
            _notificationRepository = notificationRepository;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(SetNotificationVisitedCommand request, CancellationToken cancellationToken)
        {
            var result = await _notificationRepository.SetNotificationVisited(id: request.Id, userId:_currentUser.UserId);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Unit.Value;
        }
    }
}