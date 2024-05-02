namespace BG.CampusLife.Application.Categories.Queries.GetByCode;

public class GetCategoryByCodeHandler : BaseHandler<GetCategoryByCodeHandler>, IRequestHandler<GetCategoryByCodeQuery, CategoryDto>
{
    public GetCategoryByCodeHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<CategoryDto> Handle(GetCategoryByCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.CategoryRepository.GetCategoryByCode(request.Code);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        
        return Mapper.Map<CategoryDto>(result.Entity);
    }
}