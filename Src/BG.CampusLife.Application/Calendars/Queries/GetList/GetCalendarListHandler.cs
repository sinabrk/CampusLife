namespace BG.CampusLife.Application.Calendars.Queries.GetList;

public class GetCalendarListHandler : BaseHandler<GetCalendarListHandler>, IRequestHandler<GetCalendarListQuery, List<CalendarDto>>
{
    public GetCalendarListHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    { }

    public async Task<List<CalendarDto>> Handle(GetCalendarListQuery request, CancellationToken cancellationToken)
    {
        var mainUser = await GetAndValidateUser();

        var result = await Repositories.UserCalendarRepository.GetUserCalendar(mainUser, request.Start.Value, request.End.Value);
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