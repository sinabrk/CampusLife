using System.Collections.Generic;
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

namespace BG.CampusLife.Application.Calendars.Queries.GetSharedUsers
{
    public class GetSharedCalendarUsersHandler : BaseHandler<GetSharedCalendarUsersHandler>, IRequestHandler<GetSharedCalendarUsersQuery, List<SharedCalendarDto>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IUserRepository _userRepository;
        public GetSharedCalendarUsersHandler(ILogger<GetSharedCalendarUsersHandler> logger, IMapper mapper, ICurrentUserService currentUser, ICalendarRepository repository, IUserRepository userRepository) : base(logger, mapper)
        {
            _currentUser = currentUser;
            _calendarRepository = repository;
            _userRepository = userRepository;
        }

        public async Task<List<SharedCalendarDto>> Handle(GetSharedCalendarUsersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _calendarRepository.GetSharedUsers(user.Entity);
            return Mapper.Map<List<SharedCalendarDto>>(result.Entities);
        }
    }
}