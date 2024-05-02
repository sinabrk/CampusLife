namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IUserCalendarRepository
{
    Task<UserCalendar> CreateUserCalendar(UserCalendar calendar, CancellationToken cancellationToken);
    Task<UserCalendar> UpdateUserCalendar(UserCalendar calendar, CancellationToken cancellationToken);
    Task<Result<int>> DeleteUserCalendar(Guid calendarId, string userId, CancellationToken cancellationToken);
    Task<Result<int>> ShareCalendarWithOtherUser(User user, User targetUser, CancellationToken cancellationToken);
    Task<Result<int>> RemoveSharingCalendar(User user, User targetUser, CancellationToken cancellationToken);
    Task<Result<UserCalendar>> GetUserCalendarByUserId(Guid calendarId, string userId);
    Task<Result<UserCalendar>> GetUserCalendar(User user, DateTime? startDate = null, DateTime? endDate = null);
    Task<Result<SharedCalendar>> GetSharedUsers(User user);
    Task<Result<UserCalendar>> GetSharedCalendar(Guid sharedToUserId, DateTime? startDate = null, DateTime? endDate = null);
}