namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : INotificationRepository
{
    public async Task<Result<int>> CreateNotification(Notification notification, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        _context.Set<Notification>().Add(notification);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<Result<int>> BulkCreateNotifications(List<Notification> notifications, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        _context.Set<Notification>().AddRange(notifications);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<Result<int>> SetNotificationVisited(Guid id, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        var entity = await _context.Set<Notification>()
            .Include(item => item.User)
            .Where(n => n.Id == id && n.User.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Message = MessageHelper.ErrorNotFound("Notification", $"{id.ToString()}");
            result.Succeeded = false;
            result.StatusCode = ResultStatusCodes.NotFound;
        }
        else
        {
            entity.Visited = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return result;
    }

    public async Task<Result<Notification>> GetNotificationById(Guid id, string userId)
    {
        var result = new Result<Notification>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful
        };
        var entity = await _context.Set<Notification>()
            .Include(item => item.User)
            .Where(n => n.Id == id && n.User.UserId == userId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (entity is null)
        {
            result.Message = MessageHelper.ErrorNotFound("Notification", $"{id.ToString()}");
            result.StatusCode = ResultStatusCodes.NotFound;
            result.Succeeded = false;
        }
        else
        {
            result.Entity = entity;
        }

        return result;

    }

    public async Task<Result<Notification>> GetUserNotifications(string userId)
    {
        var entities = await _context.Set<Notification>()
            .Include(item => item.User)
            .Where(n => n.User.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return new Result<Notification>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count
        };
    }
}