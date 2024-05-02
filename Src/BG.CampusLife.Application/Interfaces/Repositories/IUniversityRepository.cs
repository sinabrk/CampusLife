namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IUniversityRepository
{
    Task<Result<University>> GetUniversityList(Guid locationId, string searchText);
    Task<Result<University>> GetUniversityById(Guid id);
    Task<Result<University>> CreateOrUpdateUniversity(University university, CancellationToken cancellationToken);
    Task<Result<int>> DeleteUniversity(Guid id, CancellationToken cancellationToken);
}