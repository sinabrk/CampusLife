namespace BG.CampusLife.Application.Calendars.Queries.GetSharedCalendar;

public class GetSharedCalendarQuery : IRequest<List<CalendarDto>>
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}