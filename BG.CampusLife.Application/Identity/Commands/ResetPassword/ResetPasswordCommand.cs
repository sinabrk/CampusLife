using MediatR;

namespace BG.CampusLife.Application.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}