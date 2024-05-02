namespace BG.CampusLife.Application.Categories.Queries.GetList;

public class GetCategoriesListQuery : IRequest<List<CategoryDto>>
{
    public int? Level { get; set; }
    public CategoryTypes CategoryType { get; set; }
    public Guid? ParentId { get; set; }

    [DefaultValue(true)]
    public bool Status { get; set; }
}