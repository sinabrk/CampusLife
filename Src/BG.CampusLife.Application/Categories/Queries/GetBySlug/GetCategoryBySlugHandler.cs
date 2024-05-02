namespace BG.CampusLife.Application.Categories.Queries.GetBySlug;

public class GetCategoryBySlugHandler : BaseHandler<GetCategoryBySlugHandler>, IRequestHandler<GetCategoryBySlugQuery, CategoryDto>
{
    public GetCategoryBySlugHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<CategoryDto> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.CategoryRepository.GetCategoryBySlug(request.Slug);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<CategoryDto>(result.Entity);
    }
}