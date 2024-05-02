using System;
using BG.CampusLife.Application.Calendars.DTOs;
using BG.CampusLife.Domain.Enums;
using MediatR;

namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar
{
    public class UpsertCalendarCommand : IRequest<CalendarDto>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid EntityId { get; set; }
        
        public EntityTypes EntityType { get; set; }
        
        public string Notes { get; set; }
    }
}