namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IUniversityRepository
{
    public async Task<Result<University>> GetUniversityList(Guid locationId, string searchText)
    {
        var result = new Result<University>
        {
            StatusCode = ResultStatusCodes.Successful,
            Succeeded = true,
            Entities = await _context.Set<University>()
                .Include(c => c.Location)
                .Where(p =>
                    (locationId == Guid.Empty || p.LocationId == locationId) &&
                    (string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText)) &&
                    p.Status == true
                )
                .AsNoTracking()
                .ToListAsync()
        };

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<University>> GetUniversityById(Guid id)
    {
        var result = new Result<University>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<University>()
                .Include(c => c.Location)
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Message = MessageHelper.ErrorNotFound("University", id.ToString());
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Succeeded = false;

        return result;
    }
    
    public async Task<Result<University>> CreateOrUpdateUniversity(University university, CancellationToken cancellationToken)
    {
        var result = new Result<University>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };

        if (university.Id == Guid.Empty)
        {
            university.Id = Guid.NewGuid();
            _context.Set<University>().Add(university);
        }
        else
        {
            var entity =
                await _context.Set<University>()
                    .Where(c => c.Id == university.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.Name = university.Name;
                entity.LocationId = university.LocationId;
                entity.Status = university.Status;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("University", university.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        result.Entity = university;

        return result;
    }
    
    public async Task<Result<int>> DeleteUniversity(Guid id, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent
        };
        var entity = await _context.Set<University>()
            .Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("University", $"{id}");
            result.StatusCode = ResultStatusCodes.NotFound;
        }
        else
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}