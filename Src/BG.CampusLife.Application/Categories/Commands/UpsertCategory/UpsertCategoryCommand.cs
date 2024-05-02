namespace BG.CampusLife.Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public CategoryTypes CategoryType { get; set; }
    public int Level { get; set; }
    public Guid? ParentId { get; set; }
    public string Code { get; set; }
    public string Slug { get; set; }
    public bool Status { get; set; }
}