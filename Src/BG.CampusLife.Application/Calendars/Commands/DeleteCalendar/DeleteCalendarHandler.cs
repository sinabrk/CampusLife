namespace BG.CampusLife.Application.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarHandler : BaseHandler<DeleteCalendarHandler>, IRequestHandler<DeleteCalendarCommand>
{
    public DeleteCalendarHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(DeleteCalendarCommand request, CancellationToken cancellationToken)
    {
        Validate(request.Id);
        var result = await Repositories.UserCalendarRepository.DeleteUserCalendar(request.Id, CurrentUserService.UserId, cancellationToken);
        if (!result.Succeeded) throw new CampusException(result.Message, (int)result.StatusCode);

        return Unit.Value;
    }

    private async void Validate(Guid id)
    {
        var user = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (user.StatusCode == ResultStatusCodes.NotFound) throw new NotFoundException(user.Message);

        var userCalendar = await Repositories.GetEntityById<UserCalendar>(id);
        if (userCalendar is null) throw new NotFoundException(nameof(User), id);
    }
}