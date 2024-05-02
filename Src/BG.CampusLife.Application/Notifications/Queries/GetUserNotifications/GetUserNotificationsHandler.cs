namespace BG.CampusLife.Application.Notifications.Queries.GetUserNotifications;

public class GetUserNotificationsHandler : BaseHandler<GetUserNotificationsHandler>, IRequestHandler<GetUserNotificationsQuery, List<UserNotificationDto>>
{
    public GetUserNotificationsHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<List<UserNotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await Repositories.NotificationRepository.GetUserNotifications(CurrentUserService.UserId);
        return Mapper.Map<List<UserNotificationDto>>(notifications.Entities);
    }
}