namespace BG.CampusLife.Application.MarketPlace.Queries.GetDetail;

public class GetMarketItemDetailHandler : BaseHandler<GetMarketItemDetailHandler>, IRequestHandler<GetMarketItemDetailQuery, MarketItemDto>
{
    public GetMarketItemDetailHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<MarketItemDto> Handle(GetMarketItemDetailQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.MarketRepository.GetMarketDetail(request.Id);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<MarketItemDto>(result.Entity);
    }
}