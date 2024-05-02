namespace BG.CampusLife.Application.Identity.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshTokenDto>
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}
