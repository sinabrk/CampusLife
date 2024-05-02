using System.Linq;
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

namespace BG.CampusLife.Application.Calendars.Commands.DeleteCalendar
{
    public class DeleteCalendarHandler : BaseHandler<DeleteCalendarHandler>, IRequestHandler<DeleteCalendarCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUser;
        
        public DeleteCalendarHandler(ILogger<DeleteCalendarHandler> logger, IMapper mapper, ICalendarRepository calendarRepository, ICurrentUserService currentUserService, IUserRepository userRepository) : base(logger, mapper)
        {
            _calendarRepository = calendarRepository;
            _currentUser = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteCalendarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _calendarRepository.Delete(request.Id, user.Entity);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Unit.Value;
        }
    }
}