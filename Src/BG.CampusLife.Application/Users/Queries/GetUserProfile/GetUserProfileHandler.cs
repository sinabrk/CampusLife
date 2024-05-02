namespace BG.CampusLife.Application.Users.Queries.GetUserProfile;

public class GetUserProfileHandler : BaseHandler<GetUserProfileHandler>, IRequestHandler<GetUserProfileQuery, UserDto>
{
    public GetUserProfileHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userRole = await Repositories.UserManagerServices.GetUserRolesAsync(CurrentUserService.UserId);
        var profile = await Repositories.UserRepository.GetUserProfile(CurrentUserService.UserId, userRole.Entities.First());
        return Mapper.Map<UserDto>(profile.Entity);
    }
}