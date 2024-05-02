namespace BG.CampusLife.Application.MarketPlace.Queries.GetMarketTags;

public class GetMarketTagsHandler : BaseHandler<GetMarketTagsHandler>, IRequestHandler<GetMarketTagsQuery, List<MarketTagDto>>
{
    public GetMarketTagsHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<MarketTagDto>> Handle(GetMarketTagsQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.TagRepository.GetUsedTags();
        return Mapper.Map<List<MarketTagDto>>(result.Entities);
    }
}