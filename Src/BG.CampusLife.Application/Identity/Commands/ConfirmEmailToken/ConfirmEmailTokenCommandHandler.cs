namespace BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;

public class ConfirmEmailTokenCommandHandler : IRequestHandler<ConfirmEmailTokenCommand>
{
    private readonly IUserManager _userManager;

    public ConfirmEmailTokenCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ConfirmEmailTokenCommand request, CancellationToken cancellationToken)
    {
        request.Token = request.Token.Replace(" ", "+");
        var result = await _userManager.ConfirmEmail(request.Email, request.Token);
        if (!result.Succeeded)
            throw new InvalidTokenException();
        return Unit.Value;
    }
}