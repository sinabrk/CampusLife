namespace BG.CampusLife.Application.Users.Commands.UpdateProfile;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
{
    private readonly IUniversityRepository _universityRepository;
    private readonly ILocationRepository _locationRepository;
    public UpdateProfileValidator(IUniversityRepository universityRepository, ILocationRepository locationRepository)
    {
        _universityRepository = universityRepository;
        _locationRepository = locationRepository;
        RuleFor(c => c.UniversityId).MustAsync(UniversityExists).WithMessage("University is not valid");
        RuleFor(c => c.FirstName).NotEmpty().NotNull();
        RuleFor(c => c.LastName).NotEmpty().NotNull();
        RuleFor(c => c.LocationId).MustAsync(IsLocationExists).WithMessage("LocationId is not valid");
    }

    private async Task<bool> IsLocationExists(Guid? locationId, CancellationToken cancellationToken)
    {
        return await _locationRepository.IsLocationExists(locationId.Value);
    }

    private async Task<bool> UniversityExists(Guid? universityId, CancellationToken cancellationToken)
    {
        var entity = await _universityRepository.GetUniversityById(universityId.Value);
        return entity != null;
    }
}