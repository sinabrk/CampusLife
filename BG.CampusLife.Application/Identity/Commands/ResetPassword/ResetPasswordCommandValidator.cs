using FluentValidation;

namespace BG.CampusLife.Application.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(a => a.UserName).EmailAddress().NotEmpty().MaximumLength(50);
            RuleFor(a => a.Password).Length(8, 50).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])")
                .WithMessage("Password Is Not Strong Enough");
            RuleFor(a => a.Token).NotEmpty();

        }
    }
}
