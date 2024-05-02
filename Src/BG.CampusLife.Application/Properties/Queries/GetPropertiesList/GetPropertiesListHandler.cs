namespace BG.CampusLife.Application.Properties.Queries.GetPropertiesList;

public class GetPropertiesListHandler : BaseHandler<GetPropertiesListHandler>, IRequestHandler<GetPropertiesListQuery, List<PropertyListDto>>
{
    public GetPropertiesListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos) {}

    public async Task<List<PropertyListDto>> Handle(GetPropertiesListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PropertyRepository.GetPropertyList(searchText: request.SearchText, categoryId: request.CategoryId);
        return Mapper.Map<List<PropertyListDto>>(result.Entities);
    }
}