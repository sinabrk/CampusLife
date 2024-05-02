namespace BG.CampusLife.Application.Categories.Queries.GetList;

public class GetCategoriesListValidator : AbstractValidator<GetCategoriesListQuery>
{
    public GetCategoriesListValidator()
    {
        RuleFor(c => c.Status).NotNull().WithMessage("Please define status active/inactive");
    }
}