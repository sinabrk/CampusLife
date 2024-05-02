using BG.CampusLife.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand>
    {
        private readonly IUserManager _userManager;

        public ForgetPasswordCommandHandler(IUserManager identityManager)
        {
            _userManager = identityManager;
        }

        public async Task<Unit> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _userManager.ResetPasswordToken(request.UserName);
            return Unit.Value;
        }
    }
}