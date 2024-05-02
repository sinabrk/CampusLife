namespace BG.CampusLife.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public Guid Id { get; set; }
}