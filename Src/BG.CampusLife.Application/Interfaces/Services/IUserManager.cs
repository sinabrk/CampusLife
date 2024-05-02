namespace BG.CampusLife.Application.Interfaces.Services;

public interface IUserManager
{
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<(IdentityResultHandler result, string userId)> CreateUserAsync(string email, string password, Roles role);

    /// <summary>
    /// Get User Data
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Result<UserData>> GetUserAsync(string userId);

    /// <summary>
    /// Get User Roles
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Result<string>> GetUserRolesAsync(string userId);

    /// <summary>
    /// Add Roles to a specific user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    Task<Result<int>> AddRolesToUser(string userId, IEnumerable<string> roles);

    /// <summary>
    /// Remove Roles of a user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    Task<Result<int>> RemoveRolesFromUser(string userId, List<string> roles);

    /// <summary>
    /// Change Password of a user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="oldPass"></param>
    /// <param name="newPass"></param>
    /// <returns></returns>
    Task<Result<int>> ChangePassword(string userId, string oldPass, string newPass);

    /// <summary>
    /// Create Reset Password Token
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task<Result<int>> ResetPasswordToken(string userName);

    /// <summary>
    /// Reset User Password from admin
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task<Result<int>> ResetPassword(string userName, string newPassword);

    /// <summary>
    /// Reset user password with token
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="newPassword"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Result<int>> ResetPassword(string userName, string newPassword, string token);

    /// <summary>
    /// Confirm user email with token
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IdentityResultHandler> ConfirmEmail(string email, string token);

    /// <summary>
    /// Get List Users of a role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<Result<UserData>> GetUsers(string role);

    /// <summary>
    /// Get All Roles in identity provider
    /// </summary>
    /// <returns></returns>
    Task<Result<string>> GetRoles();

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Result<UserData>> GetUserByEmailAsync(string email);

    /// <summary>
    /// Suspend a user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Result<int>> SuspendUser(string userId);

    /// <summary>
    /// Account Deactivation
    /// User Can only deactivate account every 7 days
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Result<int>> AccountDeactivate(string userId);

    /// <summary>
    /// Update User ConnectionId
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    Task<Result<int>> UpdateUserConnectionId(string userName, string connectionId);


}