namespace BG.CampusLife.Application.Calendars.DTOs;

public class CalendarDto : IMapFrom<UserCalendar>
{
    public Guid Id { get; set; }
    public string Date { get; set; }
    public Guid EntityId { get; set; }
    public string EntityType { get; set; }
    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCalendar, CalendarDto>()
            .ForMember(cld => cld.Date,
                opt =>
                    opt.MapFrom(c => c.Date.ToString("yyyy/MM/dd HH:mm:ss")))
            .ForMember(cld => cld.EntityType,
                opt => opt.MapFrom(
                    item => item.EntityType.ToString()));
    }
}