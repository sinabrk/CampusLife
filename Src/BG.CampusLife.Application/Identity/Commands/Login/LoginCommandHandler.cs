namespace BG.CampusLife.Application.Identity.Commands.Login;

public class LoginCommandHandler : BaseHandler<LoginCommandHandler>,IRequestHandler<LoginCommand, LoginDto>
{
    public LoginCommandHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.SignInManagerServices.Login(request.Email, request.Password);
        if (!result.Succeeded) throw new UnauthorizedAccessException(result.Message);
        return Mapper.Map<LoginDto>(result.Entity);
    }
}