namespace BG.CampusLife.Application.Tags.Queries.GetTagsList;

public class GetTagsListHandler : BaseHandler<GetTagsListHandler>, IRequestHandler<GetTagsListQuery, List<TagListDto>>
{
    public GetTagsListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<TagListDto>> Handle(GetTagsListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.TagRepository.GetTagList(searchText: request.SearchText);
        return Mapper.Map<List<TagListDto>>(result.Entities);
    }
}