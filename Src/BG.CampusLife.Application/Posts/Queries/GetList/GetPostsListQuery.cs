namespace BG.CampusLife.Application.Posts.Queries.GetList;

public class GetPostsListQuery : IRequest<List<PostDto>>
{
    public Guid CategoryId { get; set; }
    public Guid LocationId { get; set; }
    public string SearchText { get; set; }
}