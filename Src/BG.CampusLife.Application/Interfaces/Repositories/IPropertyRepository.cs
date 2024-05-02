namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<Result<Property>> GetPropertyList(string searchText, Guid categoryId);
    Task<Result<Property>> GetPropertyById(Guid id);
    Task<Result<Property>> CreateOrUpdateProperty(Property property, CancellationToken cancellationToken);
    Task<Result<int>> DeleteProperty(Guid id, CancellationToken cancellationToken);
    Task<Result<Property>> GetUsedMarketProperties();
    Task<Result<MarketItemProperty>> CreateMarketItemProperties(Dictionary<string, string> properties);
}