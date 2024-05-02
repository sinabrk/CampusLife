namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IEventRepository
{
    Task<Result<Event>> GetUserEvents(Guid userId);
    Task<Result<Event>> GetEventsList();
    Task<Result<Event>> GetEventById(Guid id);
    Task<Result<Event>> CreateOrUpdateEvent(Event ev);
    Task<Result<int>> DeleteEvent(Guid id);
}