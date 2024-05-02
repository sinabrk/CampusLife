namespace BG.CampusLife.Application.Categories.Queries.GetList;

public class GetCategoriesListHandler : BaseHandler<GetCategoriesListHandler>, IRequestHandler<GetCategoriesListQuery, List<CategoryDto>>
{
    public GetCategoriesListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.CategoryRepository.GetListOfCategories(request.Level, request.CategoryType, request.ParentId, request.Status);
        return Mapper.Map<List<CategoryDto>>(result.Entities);
    }
}