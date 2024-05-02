namespace BG.CampusLife.Application.MarketPlace.Commands.UpsertMarketItem;

public class UpsertMarketItemValidator : AbstractValidator<UpsertMarketItemCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILocationRepository _locationRepository;
    public UpsertMarketItemValidator(ICategoryRepository categoryRepository, ILocationRepository locationRepository)
    {
        _categoryRepository = categoryRepository;
        _locationRepository = locationRepository;
        RuleFor(c => c.Title).NotNull().NotEmpty();
        RuleFor(c => c.Description).NotNull().NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty().NotNull().MustAsync(IsCategoryExists).WithMessage("CategoryId is Invalid");
        RuleFor(c => c.LocationId).NotEmpty().NotNull().MustAsync(IsLocationExists).WithMessage("LocationId is Invalid");
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