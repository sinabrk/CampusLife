namespace BG.CampusLife.Application.Calendars.Commands.ShareCalendar;

public class ShareCalendarHandler : BaseHandler<ShareCalendarHandler>, IRequestHandler<ShareCalendarCommand>
{
    public ShareCalendarHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(ShareCalendarCommand request, CancellationToken cancellationToken)
    {
        var (mainUser, targetUser) = await GetAndValidateUser(request.Email);

        var result = await Repositories.UserCalendarRepository.ShareCalendarWithOtherUser(mainUser, targetUser, cancellationToken);
        if (!result.Succeeded) throw new CampusException(result.Message, (int)result.StatusCode);
        
        return Unit.Value;
    }

    private async Task<(User, User)> GetAndValidateUser(string email)
    {
        var mainUser = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!mainUser.Succeeded) throw new NotFoundException(mainUser.Message);
       
        // TargetUser
        var targetUser = await Repositories.UserRepository.GetUserByEmail(email);
        if (!targetUser.Succeeded) throw new NotFoundException(targetUser.Message);

        return (mainUser.Entity, targetUser.Entity);
    }
}