namespace BG.CampusLife.Application.MarketPlace.Queries.GetDetail;

public class GetMarketItemDetailQuery : IRequest<MarketItemDto>
{
    public Guid Id { get; set; }
}