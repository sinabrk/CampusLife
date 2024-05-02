namespace BG.CampusLife.Application.Identity.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(a => a.Email).NotEmpty().MaximumLength(50);
        RuleFor(a => a.Password).Length(8, 50);
    }
}