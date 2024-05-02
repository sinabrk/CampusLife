using BG.CampusLife.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.Deactivate
{
    public class DeactivateAccountHandler : IRequestHandler<DeactivateAccountCommand>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserManager _userManager;

        public DeactivateAccountHandler(ICurrentUserService currentUser, IUserManager userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
        {
            await _userManager.AccountDeactivate(_currentUser.UserId);
            return Unit.Value;
        }
    }
}