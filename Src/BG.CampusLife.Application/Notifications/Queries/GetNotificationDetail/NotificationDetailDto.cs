namespace BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail;

public class NotificationDetailDto : IMapFrom<Notification>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool Visited { get; set; }
    public string SendDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationDetailDto>()
            .ForMember(nd => nd.SendDate,
                opt =>
                    opt.MapFrom(n => n.SendDate.ToString("g")));
    }

}