namespace BG.CampusLife.Application.Calendars.Queries.GetSharedCalendar;

public class GetSharedCalendarHandler : BaseHandler<GetSharedCalendarHandler>, IRequestHandler<GetSharedCalendarQuery, List<CalendarDto>>
{
    public GetSharedCalendarHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    { }

    public async Task<List<CalendarDto>> Handle(GetSharedCalendarQuery request, CancellationToken cancellationToken)
    {
        var mainUser = await GetAndValidateUser();

        var result = await Repositories.UserCalendarRepository.GetSharedCalendar(mainUser.Id, request.Start.Value, request.End.Value);
        if (!result.Succeeded) throw new BadRequestException(result.Message);

        return Mapper.Map<List<CalendarDto>>(result.Entities);
    }

    private async Task<User> GetAndValidateUser()
    {
        var mainUser = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!mainUser.Succeeded) throw new NotFoundException(mainUser.Message);
        return mainUser.Entity;
    }
}