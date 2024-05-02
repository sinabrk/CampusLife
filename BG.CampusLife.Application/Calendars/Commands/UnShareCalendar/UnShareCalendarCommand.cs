using MediatR;

namespace BG.CampusLife.Application.Calendars.Commands.UnShareCalendar
{
    public class UnShareCalendarCommand : IRequest
    {
        public string Email { get; set; }
    }
}