using System.Collections.Generic;
using BG.CampusLife.Application.Calendars.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Calendars.Queries.GetSharedUsers
{
    public class GetSharedCalendarUsersQuery : IRequest<List<SharedCalendarDto>>
    {
        
    }
}