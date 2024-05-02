namespace BG.CampusLife.Application.MarketPlace.Queries.GetList;

public class GetMarketItemsListQuery : IRequest<List<MarketItemsListDto>>
{
    public Guid CategoryId { get; set; }
    public Guid LocationId { get; set; }
    public List<Guid> Tags { get; set; }
    public List<Guid> Properties { get; set; }
    public bool HasImage { get; set; }
    public Guid UserId { get; set; }
}