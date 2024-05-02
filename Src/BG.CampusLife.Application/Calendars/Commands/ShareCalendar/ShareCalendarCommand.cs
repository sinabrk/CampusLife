namespace BG.CampusLife.Application.Calendars.Commands.ShareCalendar;

public class ShareCalendarCommand : IRequest
{
    public string Email { get; set; }
}