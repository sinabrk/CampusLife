namespace BG.CampusLife.Application.Calendars.Queries.GetSharedUsers;

public class GetSharedCalendarUsersHandler : BaseHandler<GetSharedCalendarUsersHandler>, IRequestHandler<GetSharedCalendarUsersQuery, List<SharedCalendarDto>>
{
    public GetSharedCalendarUsersHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<List<SharedCalendarDto>> Handle(GetSharedCalendarUsersQuery request, CancellationToken cancellationToken)
    {
        var mainUser = await GetAndValidateUser();
        var result = await Repositories.UserCalendarRepository.GetSharedUsers(mainUser);
        return Mapper.Map<List<SharedCalendarDto>>(result.Entities);
    }

    private async Task<User> GetAndValidateUser()
    {
        var mainUser = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!mainUser.Succeeded) throw new NotFoundException(mainUser.Message);
        return mainUser.Entity;
    }
}