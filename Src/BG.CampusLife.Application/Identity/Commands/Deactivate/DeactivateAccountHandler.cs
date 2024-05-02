namespace BG.CampusLife.Application.Identity.Commands.Deactivate;

public class DeactivateAccountHandler : IRequestHandler<DeactivateAccountCommand>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IUserManager _userManager;

    public DeactivateAccountHandler(ICurrentUserService currentUser, IUserManager userManager)
    {
        _currentUser = currentUser;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _userManager.AccountDeactivate(_currentUser.UserId);
        if (!result.Succeeded) throw new CampusException(result.Message, (int)result.StatusCode);
        return Unit.Value;
    }
}