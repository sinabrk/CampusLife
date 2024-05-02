namespace BG.CampusLife.Application.Posts.Queries.GetById;

public class GetPostByIdHandler : BaseHandler<GetPostByIdHandler>, IRequestHandler<GetPostByIdQuery, PostDto>
{
    public GetPostByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PostRepository.GetPostById(request.Id);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<PostDto>(result.Entity);
    }
}