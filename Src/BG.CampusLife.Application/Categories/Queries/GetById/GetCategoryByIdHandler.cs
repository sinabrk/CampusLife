namespace BG.CampusLife.Application.Categories.Queries.GetById;

public class GetCategoryByIdHandler : BaseHandler<GetCategoryByIdHandler>, IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public GetCategoryByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.CategoryRepository.GetCategoryById(request.Id);
        if (!result.Succeeded)
            throw new NotFoundException(result.Message);
        return Mapper.Map<CategoryDto>(result.Entity);
    }
}