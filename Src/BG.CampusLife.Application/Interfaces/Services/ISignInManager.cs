namespace BG.CampusLife.Application.Interfaces.Services;

public interface ISignInManager
{
    Task<Result<LoginData>> Login(string email, string password);      
    Task<Result<LoginData>> RefreshToken(string userId, string refreshToken);
}