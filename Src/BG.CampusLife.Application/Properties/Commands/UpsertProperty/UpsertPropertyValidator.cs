namespace BG.CampusLife.Application.Properties.Commands.UpsertProperty;

public class UpsertPropertyValidator : AbstractValidator<UpsertPropertyCommand>
{
    private readonly IRepositories _repository;

    public UpsertPropertyValidator(IRepositories repository)
    {
        _repository = repository;
        RuleFor(c => c.CategoryId).NotEmpty().NotNull().MustAsync(IsCategoryExists).WithMessage("CategoryId is Invalid");
        RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(250);
    }

    private async Task<bool> IsCategoryExists(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _repository.CategoryRepository.IsCategoryExists(categoryId);
    }
}