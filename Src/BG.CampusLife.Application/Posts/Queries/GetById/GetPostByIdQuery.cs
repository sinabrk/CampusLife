namespace BG.CampusLife.Application.Posts.Queries.GetById;

public class GetPostByIdQuery : IRequest<PostDto>
{
    public Guid Id { get; set; }   
}