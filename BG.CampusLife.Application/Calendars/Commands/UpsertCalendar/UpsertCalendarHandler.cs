using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Calendars.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar
{
    public class UpsertCalendarHandler : BaseHandler<UpsertCalendarHandler>, IRequestHandler<UpsertCalendarCommand, CalendarDto>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;
        public UpsertCalendarHandler(ILogger<UpsertCalendarHandler> logger, IMapper mapper, ICalendarRepository calendarRepository, ICurrentUserService currentUserService, IUserRepository userRepository) : base(logger, mapper)
        {
            _calendarRepository = calendarRepository;
            _currentUser = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<CalendarDto> Handle(UpsertCalendarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _calendarRepository.GetById(request.Id, user.Entity);
            if (result.Entity is null)
            {
                result.Entity = new Domain.Entities.Calendar()
                {
                    UserId = user.Entity.Id,
                    Date = request.Date,
                    EntityId = request.EntityId,
                    EntityType = request.EntityType,
                    Notes = request.Notes,
                };
            }
            else
            {
                result.Entity.Date = request.Date;
                result.Entity.EntityId = request.EntityId;
                result.Entity.EntityType = request.EntityType;
                result.Entity.Notes = request.Notes;
            }
            await _calendarRepository.Upsert(result.Entity);
            return Mapper.Map<CalendarDto>(result.Entity);
        }
    }
}