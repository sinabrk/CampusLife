namespace BG.CampusLife.Application.Notifications.Commands.SetNotificationVisited;

public class SetNotificationVisitedHandler : BaseHandler<SetNotificationVisitedHandler>, IRequestHandler<SetNotificationVisitedCommand>
{
    public SetNotificationVisitedHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(SetNotificationVisitedCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.NotificationRepository.SetNotificationVisited(id: request.Id, userId: CurrentUserService.UserId, cancellationToken);
        if (!result.Succeeded)
            throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}