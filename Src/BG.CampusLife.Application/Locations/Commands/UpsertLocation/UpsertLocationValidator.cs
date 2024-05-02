namespace BG.CampusLife.Application.Locations.Commands.UpsertLocation;

public class UpsertLocationValidator : AbstractValidator<UpsertLocationCommand>
{
    private readonly ILocationRepository _locationRepository;
    public UpsertLocationValidator(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Level).GreaterThan((byte)0).LessThanOrEqualTo((byte)3);
        RuleFor(c => c.ParentId).MustAsync(IsParentExists).WithMessage("ParentId is Invalid");
    }
    
    private async Task<bool> IsParentExists(Guid? parentId, CancellationToken cancellationToken)
    {
        if (!parentId.HasValue)
            return true;
        
        return await _locationRepository.IsLocationExists(parentId.Value);
    }
}