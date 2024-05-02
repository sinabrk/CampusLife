using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserManager _userManager;

        public ResetPasswordCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            request.Token = request.Token.Replace(" ", "+");
            var result = await _userManager.ResetPassword(request.UserName, request.Password, request.Token);
            if (!result.Succeeded)
                throw new InvalidTokenException();
            return Unit.Value;
        }
    }
}
