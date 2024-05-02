namespace BG.CampusLife.Application.Posts.Commands.UpsertPost;

public class UpsertPostValidator : AbstractValidator<UpsertPostCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILocationRepository _locationRepository;
    public UpsertPostValidator(ICategoryRepository categoryRepository, ILocationRepository locationRepository)
    {
        _categoryRepository = categoryRepository;
        _locationRepository = locationRepository;
        RuleFor(post => post.Title).NotEmpty().NotNull();
        RuleFor(post => post.Body).NotEmpty().NotNull();
        RuleFor(post => post.CategoryId).NotEmpty().NotNull().MustAsync(IsCategoryExists).WithMessage("CategoryId is Invalid");
        RuleFor(post => post.LocationId).NotEmpty().NotNull().MustAsync(IsLocationExists).WithMessage("LocationId is Invalid");
    }
    
    
    private async Task<bool> IsCategoryExists(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _categoryRepository.IsCategoryExists(categoryId);
    }
    
    private async Task<bool> IsLocationExists(Guid locationId, CancellationToken cancellationToken)
    {
        return await _locationRepository.IsLocationExists(locationId);
    }
}
