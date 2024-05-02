namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface ILocationRepository
{
    Task<Result<Location>> GetLocationsList(int? level, Guid? parentId, bool status);
    Task<Result<Location>> GetLocationById(Guid id);
    Task<Result<Location>> CreateOrUpdateLocation(Location location, CancellationToken cancellationToken);
    Task<Result<int>> DeleteLocation(Guid id, CancellationToken cancellationToken);
    Task<Result<Location>> GetLocationByLongLat(double longitude, double latitude);
    Task<bool> IsLocationExists(Guid id);
}
