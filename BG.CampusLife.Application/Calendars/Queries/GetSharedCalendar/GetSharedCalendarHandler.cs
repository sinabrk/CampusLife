using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Calendars.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Calendars.Queries.GetSharedCalendar
{
    public class GetSharedCalendarHandler : BaseHandler<GetSharedCalendarHandler>,
        IRequestHandler<GetSharedCalendarQuery, List<CalendarDto>>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;

        public GetSharedCalendarHandler(ILogger<GetSharedCalendarHandler> logger, IMapper mapper,
            ICalendarRepository calendarRepository, ICurrentUserService currentUserService, IUserRepository userRepository) : base(logger, mapper)
        {
            _calendarRepository = calendarRepository;
            _currentUser = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<List<CalendarDto>> Handle(GetSharedCalendarQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Calendar> calendars;
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (request.Start.HasValue && request.End.HasValue)
            {
                var result = await _calendarRepository.GetSharedCalendar(sharedToUserId: user.Entity.Id,
                    start: request.Start.Value, end: request.End.Value);
                if (!result.Succeeded)
                    throw new BadRequestException(result.Message);

                calendars = result.Entities;
            }
            else if (!request.Start.HasValue && !request.End.HasValue)
            {
                var result = await _calendarRepository.GetSharedCalendar(sharedToUserId: user.Entity.Id);
                if (!result.Succeeded)
                    throw new BadRequestException(result.Message);
                calendars = result.Entities;
            }
            else
            {
                throw new BadRequestException("Please define either all dates or none of them.");
            }
            return Mapper.Map<List<CalendarDto>>(calendars);
        }
    }
}