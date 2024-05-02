namespace BG.CampusLife.Application.Common;

public class BaseHandler<T>
{
    public BaseHandler
        (IMapper mapper, IRepositories repos, ICurrentUserService currentUserService = null)
    {
        Mapper = mapper;
        Repositories = repos;
        CurrentUserService = currentUserService;
    }

    protected IMapper Mapper { get; init; }
    protected IRepositories Repositories { get; set; }
    protected ICurrentUserService CurrentUserService { get; set; }
}
