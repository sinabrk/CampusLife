namespace BG.CampusLife.Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryValidator : AbstractValidator<UpsertCategoryCommand>
{
    public UpsertCategoryValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Level).GreaterThan((byte)0).LessThanOrEqualTo((byte)3);
    }

    public static async Task Validation(UpsertCategoryCommand dto, IRepositories repos)
    {
        if (await repos.GetEntityById<Category>(dto.ParentId.Value) == null)
            throw new NotFoundException("ParentCategory", dto.ParentId.Value);
    }
}