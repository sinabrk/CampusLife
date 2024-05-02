namespace BG.CampusLife.Application.Categories.Queries.GetBySlug;

public class GetCategoryBySlugQuery : IRequest<CategoryDto>
{
    public string Slug { get; set; }
}