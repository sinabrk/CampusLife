namespace BG.CampusLife.Application.Calendars.DTOs;

public class SharedCalendarDto : IMapFrom<SharedCalendar>
{
    public Guid Id { get; set; }
    public string Created { get; set; }
    public Guid SharedUserId { get; set; }
    public string SharedUsername { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SharedCalendar, SharedCalendarDto>()
            .ForMember(cld => cld.Created,
                opt => 
                    opt.MapFrom(c => c.Created.ToString("yyyy/MM/dd HH:mm:ss")))
            .ForMember(cld => cld.SharedUsername,
                opt => 
                    opt.MapFrom(c => MessageHelper.UserPresenter(c.Shared)));
    }
}