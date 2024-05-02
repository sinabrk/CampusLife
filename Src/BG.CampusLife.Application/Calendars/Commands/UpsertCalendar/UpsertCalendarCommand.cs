namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar;

public class UpsertCalendarCommand : IRequest<CalendarDto>
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTime Date { get; set; }

    public Guid EntityId { get; set; }
    
    public EntityTypes EntityType { get; set; }
    
    [MaxLength(250)]
    public string Notes { get; set; }
}