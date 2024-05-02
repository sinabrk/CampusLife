namespace BG.CampusLife.Application.Universities.Queries.GetList;

public class GetUniversitiesListHandler : BaseHandler<GetUniversitiesListHandler>, IRequestHandler<GetUniversitiesListQuery, List<UniversityDto>>
{
    public GetUniversitiesListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<UniversityDto>> Handle(GetUniversitiesListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.UniversityRepository.GetUniversityList(request.LocationId, request.SearchText);
        return Mapper.Map<List<UniversityDto>>(result.Entities);
    }
}