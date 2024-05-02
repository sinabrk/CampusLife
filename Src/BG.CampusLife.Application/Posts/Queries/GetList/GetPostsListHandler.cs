namespace BG.CampusLife.Application.Posts.Queries.GetList;

public class GetPostsListHandler : BaseHandler<GetPostsListHandler>, IRequestHandler<GetPostsListQuery, List<PostDto>>
{
    public GetPostsListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<PostDto>> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PostRepository.GetPostList(request.CategoryId, request.LocationId, request.SearchText);
        return Mapper.Map<List<PostDto>>(result.Entities);
    }
}