namespace BG.CampusLife.Application.Identity.Commands.RefreshToken;

public class RefreshTokenDto : IMapFrom<LoginData>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginData, RefreshTokenDto>();
    }
}
