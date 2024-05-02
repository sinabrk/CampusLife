namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Result<User>> GetUserById(string userId);
    Task<Result<User>> GetUserByEmail(string email);
    Task<Result<User>> GetUserProfile(string userId, string role);
    Task<Result<User>> GetUserList(List<UserData> users, Guid locationId, Guid universityId);
    Task<Result<User>> CreateUser(User user, CancellationToken cancellationToken);
    Task<Result<User>> UpdateUserProfile(User user, CancellationToken cancellationToken);
}