namespace BG.CampusLife.Application.Categories.Queries.GetById;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
}
