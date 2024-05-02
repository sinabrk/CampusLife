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

namespace BG.CampusLife.Application.Calendars.Queries.GetList
{
    public class GetCalendarListHandler : BaseHandler<GetCalendarListHandler>,
        IRequestHandler<GetCalendarListQuery, List<CalendarDto>>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;

        public GetCalendarListHandler(ILogger<GetCalendarListHandler> logger, IMapper mapper,
            ICalendarRepository calendarRepository, ICurrentUserService currentUserService, IUserRepository userRepository) : base(logger, mapper)
        {
            _calendarRepository = calendarRepository;
            _currentUser = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<List<CalendarDto>> Handle(GetCalendarListQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            List<Domain.Entities.Calendar> calendars;
            if (request.Start.HasValue && request.End.HasValue)
            {
                var result = await _calendarRepository.GetList(user: user.Entity,
                    start: request.Start.Value, end: request.End.Value);
                calendars = result.Entities;
            }
            else if (!request.Start.HasValue && !request.End.HasValue)
            {
                var result = await _calendarRepository.GetList(user: user.Entity);
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