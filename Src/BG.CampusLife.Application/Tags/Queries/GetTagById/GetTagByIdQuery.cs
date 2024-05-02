namespace BG.CampusLife.Application.Tags.Queries.GetTagById;

public class GetTagByIdQuery : IRequest<TagDto>
{
    public Guid Id { get; set; }
}