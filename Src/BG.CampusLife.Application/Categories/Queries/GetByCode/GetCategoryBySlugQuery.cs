namespace BG.CampusLife.Application.Categories.Queries.GetByCode;

public class GetCategoryByCodeQuery : IRequest<CategoryDto>
{
    public string Code { get; set; }
}