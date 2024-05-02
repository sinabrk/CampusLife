namespace BG.CampusLife.Application.Tags.Queries.GetTagById;

public class GetTagByIdHandler : BaseHandler<GetTagByIdHandler>, IRequestHandler<GetTagByIdQuery, TagDto>
{
    public GetTagByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.TagRepository.GetTagById(request.Id);
        return Mapper.Map<TagDto>(result.Entities);
    }
}