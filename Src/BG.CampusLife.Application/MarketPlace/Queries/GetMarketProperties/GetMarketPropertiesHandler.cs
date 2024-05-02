namespace BG.CampusLife.Application.MarketPlace.Queries.GetMarketProperties;

public class GetMarketPropertiesHandler : BaseHandler<GetMarketPropertiesHandler>, IRequestHandler<GetMarketPropertiesQuery, List<MarketPropertyDto>>
{
    public GetMarketPropertiesHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<MarketPropertyDto>> Handle(GetMarketPropertiesQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PropertyRepository.GetUsedMarketProperties();
        return Mapper.Map<List<MarketPropertyDto>>(result.Entities);
    }
}