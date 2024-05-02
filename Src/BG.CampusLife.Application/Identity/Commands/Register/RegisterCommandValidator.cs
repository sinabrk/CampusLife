namespace BG.CampusLife.Application.Identity.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{

    public RegisterCommandValidator()
    {
        RuleFor(a => a.Email).EmailAddress().NotEmpty().MaximumLength(50);
        RuleFor(a => a.Password).Length(8, 50).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])")
            .WithMessage("Password Is Not Strong Enough");
        RuleFor(a => a.FirstName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(a => a.LastName).NotNull().NotEmpty().MaximumLength(50);
    }

}
