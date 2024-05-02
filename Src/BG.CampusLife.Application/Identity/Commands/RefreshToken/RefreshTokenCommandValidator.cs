namespace BG.CampusLife.Application.Identity.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(a => a.UserId).Length(36);
        RuleFor(a => a.RefreshToken).Length(36);
    }
}