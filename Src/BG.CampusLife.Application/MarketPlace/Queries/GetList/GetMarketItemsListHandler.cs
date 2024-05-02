namespace BG.CampusLife.Application.MarketPlace.Queries.GetList;

public class GetMarketItemsListHandler : BaseHandler<GetMarketItemsListHandler>, IRequestHandler<GetMarketItemsListQuery, List<MarketItemsListDto>>
{
    public GetMarketItemsListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<MarketItemsListDto>> Handle(GetMarketItemsListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.MarketRepository.GetMarketsList(
            categoryId: request.CategoryId, locationId: request.LocationId, tags: request.Tags,
            hasImage: request.HasImage, userId: request.UserId, properties: request.Properties);

        return Mapper.Map<List<MarketItemsListDto>>(result.Entities);
    }
}