namespace BG.CampusLife.Application.Universities.Commands.UpsertUniversity;

public class UpsertUniversityValidator : AbstractValidator<UpsertUniversityCommand>
{
    private readonly ILocationRepository _locationRepository;
    public UpsertUniversityValidator(ICategoryRepository categoryRepository, ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
        RuleFor(post => post.Name).NotEmpty().NotNull();
        RuleFor(post => post.LocationId).NotEmpty().NotNull().MustAsync(IsLocationExists).WithMessage("LocationId is Invalid");
    }
    
    private async Task<bool> IsLocationExists(Guid locationId, CancellationToken cancellationToken)
    {
        return await _locationRepository.IsLocationExists(locationId);
    }
}
