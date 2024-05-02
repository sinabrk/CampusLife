namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IEventRepository
{
    public async Task<Result<Event>> GetEventsList()
    {
        var entities = await _context.Set<Event>().Select(e => new Event()
        {
            Id = e.Id,
            Title = e.Title,
            Body = e.Body,
            Created = e.Created,
            UserId = e.UserId,
            LocationId = e.LocationId,
            Start = e.Start,
            End = e.End,
        }).ToListAsync();
        
        return new Result<Event>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count,
        };;
    }

    public async Task<Result<Event>> GetEventById(Guid id)
    {
        var result = new Result<Event>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Event>().FirstOrDefaultAsync(e => e.Id == id),
        };
        if (result.Entity is not null) return result;
        
        result.Message = $"Event not found with id {id}";
        result.Succeeded = false;
        result.StatusCode = ResultStatusCodes.NotFound;

        return result;
    }

    public Task<Result<Event>> CreateOrUpdateEvent(Event ev)
    {
        throw new NotImplementedException();
    }

    public Task<Result<int>> DeleteEvent(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Event>> GetUserEvents(Guid userId)
    {
        var entities = await _context.Set<Event>().Where(e => e.UserId == userId).Select(e => new Event()
        {
            Id = e.Id,
            Title = e.Title,
            Body = e.Body,
            Created = e.Created,
            UserId = e.UserId,
            LocationId = e.LocationId,
            Start = e.Start,
            End = e.End,
        }).ToListAsync();

        return new Result<Event>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count
        };
    }
}