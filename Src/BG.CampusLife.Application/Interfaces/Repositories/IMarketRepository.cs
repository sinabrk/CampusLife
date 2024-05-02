namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IMarketRepository
{
    Task<Result<MarketItem>> GetMarketsList(Guid categoryId = default, Guid locationId = default, List<Guid> tags = null, bool hasImage = false, Guid userId = default, List<Guid> properties = null);
    Task<Result<MarketItem>> GetMarketDetail(Guid id);
    Task<Result<MarketItem>> CreateOrUpdateMarket(MarketItem marketItem, string userId, CancellationToken cancellationToken);
    Task<Result<int>> DeleteMarket(Guid id, string userId, CancellationToken cancellationToken);
}