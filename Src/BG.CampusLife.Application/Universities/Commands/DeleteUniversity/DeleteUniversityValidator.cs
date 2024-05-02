namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity;

public class DeleteUniversityValidator : AbstractValidator<DeleteUniversityCommand>
{
    public DeleteUniversityValidator()
    {
        RuleFor(university => university.Id).NotEmpty().NotNull();
    }
}
