using MediatR;

namespace BG.CampusLife.Application.Identity.Commands.Login
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
