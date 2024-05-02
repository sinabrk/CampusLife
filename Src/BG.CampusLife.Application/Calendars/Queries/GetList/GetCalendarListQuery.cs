namespace BG.CampusLife.Application.Calendars.Queries.GetList;

public class GetCalendarListQuery : IRequest<List<CalendarDto>>
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}