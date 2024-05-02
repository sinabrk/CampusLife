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

namespace BG.CampusLife.Application.Calendars.Commands.ShareCalendar
{
    public class ShareCalendarHandler : BaseHandler<ShareCalendarHandler>, IRequestHandler<ShareCalendarCommand>
    {
        private readonly ICalendarRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;
        public ShareCalendarHandler(ILogger<ShareCalendarHandler> logger, IMapper mapper, ICalendarRepository repository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _repository = repository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ShareCalendarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var targetUser = await _userRepository.GetByEmail(request.Email);
            if (!targetUser.Succeeded) throw new NotFoundException(targetUser.Message);
            
            var result = await _repository.ShareCalendar(user.Entity, targetUser.Entity);
            if (!result.Succeeded)
                throw new CampusException(result.Message, (int)result.StatusCode);
            return Unit.Value;
        }
    }
}