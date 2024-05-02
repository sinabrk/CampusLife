namespace BG.CampusLife.Application.Posts.Commands.UpsertPost;

public class UpsertPostCommand : IRequest<PostDto>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public Guid CategoryId { get; set; }

    public Guid LocationId { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public List<Guid> Images { get; set; } = new List<Guid>();
}
