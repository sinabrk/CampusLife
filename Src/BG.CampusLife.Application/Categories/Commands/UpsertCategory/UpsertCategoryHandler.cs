namespace BG.CampusLife.Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryHandler : BaseHandler<UpsertCategoryHandler>, IRequestHandler<UpsertCategoryCommand, CategoryDto>
{
    public UpsertCategoryHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    { }

    public async Task<CategoryDto> Handle(UpsertCategoryCommand dto, CancellationToken cancellationToken)
    {
        if (dto.ParentId != null && dto.ParentId != Guid.Empty)
            await UpsertCategoryValidator.Validation(dto, Repositories);

        if (dto.Id == Guid.Empty)
            return Mapper.Map<CategoryDto>(await Repositories.CategoryRepository.CreateCategory(dto, cancellationToken));
        
        if (Repositories.GetEntityById<Category>(dto.Id) == null)
            throw new NotFoundException(nameof(Category), dto.Id);

        return Mapper.Map<CategoryDto>(await Repositories.CategoryRepository.UpdateCategory(dto, cancellationToken));
    }
}