using BG.CampusLife.Application.Calendars.Queries.GetSharedUsers;
using BG.CampusLife.Application.Common;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IUserCalendarRepository
{
    public async Task<Result<UserCalendar>> GetUserCalendarByUserId(Guid calendarId, string userId)
    {
        var result = new Result<UserCalendar>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<UserCalendar>()
                .Include(item => item.User)
                .Where(item => item.Id == calendarId && item.User.UserId == userId)
                .FirstOrDefaultAsync()
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Message = MessageHelper.ErrorNotFound("UserCalendar", $"{calendarId} or {userId}");

        return result;
    }

    public async Task<Result<UserCalendar>> GetUserCalendar(User user)
    {
        var entities = await _context.Set<UserCalendar>()
            .Include(item => item.User)
            .Where(c => c.UserId == user.Id)
            .AsNoTracking()
            .ToListAsync();

        return new Result<UserCalendar>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count,
        };
    }

    public async Task<Result<UserCalendar>> GetUserCalendarByDate(User user, DateTime start, DateTime end)
    {
        var entities = await _context.Set<UserCalendar>()
            .Include(item => item.User)
            .Where(
                c => c.UserId == user.Id &&
                     c.Date >= start &&
                     c.Date < end
            )
            .AsNoTracking()
            .ToListAsync();

        return new Result<UserCalendar>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count,
        };
    }

    public async Task<UserCalendar> CreateUserCalendar(UserCalendar userCalendarDto, CancellationToken cancellationToken)
    {
        userCalendarDto.Id = Guid.NewGuid();
        _context.Set<UserCalendar>().Add(userCalendarDto);
        await _context.SaveChangesAsync(cancellationToken);

        return userCalendarDto;
    }

    public async Task<UserCalendar> UpdateUserCalendar(UserCalendar dto, CancellationToken cancellationToken)
    {
        var userCalendar = await _context.Set<UserCalendar>()
            .Where(c => c.Id == dto.Id && c.UserId == dto.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        MaptoUserCalendar(dto, userCalendar);

        return userCalendar;
    }

    private static void MaptoUserCalendar(UserCalendar userCalendar, UserCalendar dto)
    {
        userCalendar.Date = dto.Date;
        userCalendar.Notes = dto.Notes;
        userCalendar.EntityId = dto.EntityId;
        userCalendar.EntityType = dto.EntityType;
    }

    public async Task<Result<int>> ShareCalendarWithOtherUser(User user, User targetUser, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful
        };

        if (await _context.Set<SharedCalendar>()
            .Where(s =>
                s.UserId == user.Id &&
                s.SharedUserId == targetUser.Id)
            .AsNoTracking()
            .AnyAsync(cancellationToken))
        {
            result.Message = $"UserCalendar already shared to this user.";
            result.StatusCode = ResultStatusCodes.BadRequest;
            result.Succeeded = false;
        }
        else
        {
            _context.Set<SharedCalendar>().Add(new SharedCalendar()
            {
                UserId = user.Id,
                SharedUserId = targetUser.Id,
            });
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public async Task<Result<int>> RemoveSharingCalendar(User user, User targetUser, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful
        };

        var entity =
            await _context.Set<SharedCalendar>()
                .Where(c =>
                    c.UserId == user.Id &&
                    c.SharedUserId == targetUser.Id)
                .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Message = $"UserCalendar is not shared to this user.";
            result.StatusCode = ResultStatusCodes.BadRequest;
            result.Succeeded = false;
        }
        else
        {
            _context.Set<SharedCalendar>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public async Task<Result<SharedCalendar>> GetSharedUsers(User user)
    {
        var entities = await _context.Set<SharedCalendar>()
            .Include(s => s.Shared)
            .Where(s => s.UserId == user.Id)
            .AsNoTracking()
            .ToListAsync();

        return new Result<SharedCalendar>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count,
        };
    }

    public async Task<Result<UserCalendar>> GetSharedCalendar(Guid sharedToUserId)
    {
        var result = new Result<UserCalendar>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
        };
        var entity = await _context.Set<SharedCalendar>()
            .Where(c => c.SharedUserId == sharedToUserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (entity is null)
        {
            result.Message = "No UserCalendar is shared to this user !";
            result.StatusCode = ResultStatusCodes.BadRequest;
            result.Succeeded = false;
            return result;
        }

        result.Entities =
            await _context.Set<UserCalendar>()
                .Include(s => s.User)
                .Where(c => c.UserId == entity.UserId)
                .AsNoTracking()
                .ToListAsync();

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<int>> DeleteUserCalendar(Guid calendarId, string userId, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<UserCalendar>()
            .Where(c => c.Id == calendarId && c.User.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

        entity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>() { Succeeded = true };
    }

    public async Task<Result<UserCalendar>> GetUserCalendar(User user, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Set<UserCalendar>()
          .Include(item => item.User)
          .Where(c => c.UserId == user.Id)
          .AsNoTracking();

        if (startDate != null && endDate != null)
            query.Where(x => x.Date >= startDate && x.Date <= endDate);

        var result = await query.ToListAsync();

        return new Result<UserCalendar>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = result,
            Total = result.Count,
        };
    }

    public async Task<Result<UserCalendar>> GetSharedCalendar(Guid sharedToUserId, DateTime? startDate, DateTime? endDate)
    {
        var userId = GetSharedUser(sharedToUserId);
        var userCalendar = _context.Set<UserCalendar>()
            .Include(item => item.User)
            .Where(c => c.UserId == userId)
            .AsNoTracking();

        if (startDate != null)
            userCalendar.Where(x => x.Date >= startDate);
        if (endDate != null)
            userCalendar.Where(x => x.Date < endDate);

        var result = await userCalendar.ToListAsync();

        return new Result<UserCalendar>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = result,
            Total = result.Count
        };
    }

    private Guid GetSharedUser(Guid sharedToUserId)
    {
        var userId = _context.Set<SharedCalendar>()
            .Where(c => c.SharedUserId == sharedToUserId)
            .Select(x => x.UserId)
            .FirstOrDefault();

        return userId == Guid.Empty 
            ? throw new NotFoundException($"No calendar is shared to this user. User id = '{sharedToUserId}'")
            : userId;
    }
}