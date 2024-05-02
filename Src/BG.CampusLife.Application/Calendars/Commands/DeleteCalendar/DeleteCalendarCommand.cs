namespace BG.CampusLife.Application.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarCommand : IRequest
{
    public Guid Id { get; set; }
}