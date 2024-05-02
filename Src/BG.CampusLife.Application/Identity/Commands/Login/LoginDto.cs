namespace BG.CampusLife.Application.Identity.Commands.Login;

public class LoginDto : IMapFrom<LoginData>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginData, LoginDto>();
    }
}
