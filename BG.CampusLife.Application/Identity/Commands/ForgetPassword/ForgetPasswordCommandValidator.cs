using FluentValidation;

namespace BG.CampusLife.Application.Identity.Commands.ForgetPassword
{
    public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(a => a.UserName).EmailAddress().NotEmpty().MaximumLength(50);
        }
    }
}