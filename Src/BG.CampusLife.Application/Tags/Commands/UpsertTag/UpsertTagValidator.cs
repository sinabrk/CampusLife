namespace BG.CampusLife.Application.Tags.Commands.UpsertTag;

public class UpsertTagValidator : AbstractValidator<UpsertTagCommand>
{
    public UpsertTagValidator()
    {
        RuleFor(c => c.Title).NotEmpty().NotNull().MaximumLength(100);
    }
}