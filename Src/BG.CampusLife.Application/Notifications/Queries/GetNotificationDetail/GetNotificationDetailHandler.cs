namespace BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail;

public class GetNotificationDetailHandler : BaseHandler<GetNotificationDetailHandler>, IRequestHandler<GetNotificationDetailQuery, NotificationDetailDto>
{
    public GetNotificationDetailHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<NotificationDetailDto> Handle(GetNotificationDetailQuery request, CancellationToken cancellationToken)
    {   
        var result = await Repositories.NotificationRepository.GetNotificationById(request.Id, CurrentUserService.UserId);
        if (!result.Succeeded)
            throw new NotFoundException(result.Message);
        return Mapper.Map<NotificationDetailDto>(result.Entity);
    }
}