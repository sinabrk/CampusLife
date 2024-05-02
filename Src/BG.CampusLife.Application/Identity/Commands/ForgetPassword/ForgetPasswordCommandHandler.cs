namespace BG.CampusLife.Application.Identity.Commands.ForgetPassword;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand>
{
    private readonly IUserManager _userManager;

    public ForgetPasswordCommandHandler(IUserManager identityManager)
    {
        _userManager = identityManager;
    }

    public async Task<Unit> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _userManager.ResetPasswordToken(request.UserName);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}