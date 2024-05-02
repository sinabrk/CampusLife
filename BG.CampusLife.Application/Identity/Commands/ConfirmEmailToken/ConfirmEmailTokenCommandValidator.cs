using FluentValidation;

namespace BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken
{
    public class ConfirmEmailTokenCommandValidator : AbstractValidator<ConfirmEmailTokenCommand>
    {
        public ConfirmEmailTokenCommandValidator()
        {
            RuleFor(a => a.Email).EmailAddress().NotEmpty().MaximumLength(50);
            RuleFor(a => a.Token).NotEmpty();
        }
    }
}