namespace BG.CampusLife.Application.MarketPlace.Commands.UpsertMarketItem;

public class UpsertMarketItemCommand : IRequest<MarketItemDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public Guid LocationId { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    public List<Guid> Images { get; set; } = new List<Guid>();
}