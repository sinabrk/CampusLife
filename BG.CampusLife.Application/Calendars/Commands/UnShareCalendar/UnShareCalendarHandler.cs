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

namespace BG.CampusLife.Application.Calendars.Commands.UnShareCalendar
{
    public class UnShareCalendarHandler : BaseHandler<UnShareCalendarHandler>, IRequestHandler<UnShareCalendarCommand>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        public UnShareCalendarHandler(ILogger<UnShareCalendarHandler> logger, IMapper mapper, ICalendarRepository repository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _calendarRepository = repository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UnShareCalendarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var targetUser = await _userRepository.GetByEmail(request.Email);
            if (!targetUser.Succeeded) throw new NotFoundException(targetUser.Message);

            
            var result = await _calendarRepository.UnShareCalendar(user.Entity, targetUser.Entity);
            if (!result.Succeeded)
                throw new CampusException(result.Message, (int)result.StatusCode);
            
            return Unit.Value;
        }
    }
}