namespace BG.CampusLife.Application.Identity.Commands.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler<RefreshTokenCommandHandler>,IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    public RefreshTokenCommandHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.SignInManagerServices.RefreshToken(request.UserId, request.RefreshToken);
        if (!result.Succeeded) throw new UnauthorizedAccessException(result.Message);
        return Mapper.Map<RefreshTokenDto>(result.Entity);
    }
}
