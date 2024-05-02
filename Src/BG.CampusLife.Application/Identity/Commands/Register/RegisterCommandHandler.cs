namespace BG.CampusLife.Application.Identity.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserManager userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (result, userId) =
            await _userManager.CreateUserAsync(request.Email, request.Password, request.Role);

        if (!result.Succeeded) throw new RegistrationException(result.Errors);

        await _userRepository.CreateUser(new User()
        {
            UserId = userId,
            UniversityId = request.UniversityId,
            LocationId = request.LocationId,
            Gender = request.Gender,
            Bio = "",
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
        }, cancellationToken);

        return Unit.Value;
    }
}