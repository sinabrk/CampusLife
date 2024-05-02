using BG.CampusLife.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserManager _userManager;

        public RegisterCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var (result, userId) =
                await _userManager.CreateUserAsync(request.Email, request.Password, request.Role, request.LocationId, request.UniversityId, request.Gender, request.FirstName, request.LastName);

            return Unit.Value;
        }
    }
}