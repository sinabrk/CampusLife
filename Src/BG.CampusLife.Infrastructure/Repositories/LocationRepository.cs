namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : ILocationRepository
{
    public async Task<Result<Location>> GetLocationsList(int? level, Guid? parentId, bool status)
    {
        var result = new Result<Location>
        {
            StatusCode = ResultStatusCodes.Successful,
            Succeeded = true,
            Entities = await _context.Set<Location>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(
                    c => (!level.HasValue || c.Level == level) &&
                         (parentId.HasValue ? c.ParentId == parentId.Value : c.ParentId == null) &&
                         c.Status == status
                )
                .AsNoTracking()
                .ToListAsync()
        };

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<Location>> GetLocationById(Guid id)
    {
        var result = new Result<Location>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Location>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync()
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.Message = MessageHelper.ErrorNotFound("Location", id.ToString());
        result.StatusCode = ResultStatusCodes.NotFound;
        return result;
    }

    public async Task<Result<Location>> CreateOrUpdateLocation(Location location, CancellationToken cancellationToken)
    {
        var result = new Result<Location>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };
        if (location.Id == Guid.Empty)
        {
            location.Id = Guid.NewGuid();
            _context.Set<Location>().Add(location);
        }
        else
        {
            var entity =
                await _context.Set<Location>()
                    .Where(c => c.Id == location.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.Title = location.Title;
                entity.Level = location.Level;
                entity.ParentId = location.ParentId;
                entity.Longitude = location.Longitude;
                entity.Latitude = location.Latitude;
                entity.Status = location.Status;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("Location", location.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        result.Entity = location;

        return result;
    }

    public async Task<Result<Location>> GetLocationByLongLat(double longitude, double latitude)
    {
        var result = new Result<Location>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Location>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(x =>
                    Math.Abs(x.Longitude - longitude) < 1 &&
                    Math.Abs(x.Latitude - latitude) < 1
                )
                .AsNoTracking()
                .FirstOrDefaultAsync()
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.Message = MessageHelper.ErrorNotFound("Location", $"{longitude} and {longitude}");
        result.StatusCode = ResultStatusCodes.NotFound;
        return result;
    }

    public async Task<Result<int>> DeleteLocation(Guid id, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent
        };
        var entity = await _context.Set<Location>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("Location", id.ToString());
            result.StatusCode = ResultStatusCodes.NotFound;
        }
        else if (await IsLocationParent(id))
        {
            result.Succeeded = false;
            result.Message = $"Location with {id} is a parent entity.";
            result.StatusCode = ResultStatusCodes.BadRequest;
        }
        else
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public async Task<bool> IsLocationExists(Guid id) => await _context.Set<TempDocument>().AnyAsync(c => c.Id == id);

    private async Task<bool> IsLocationParent(Guid id) => await _context.Set<Location>().AnyAsync(c => c.ParentId == id);
}