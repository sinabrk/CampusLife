namespace BG.CampusLife.Application.Locations.Queries.GetList;

public class GetLocationsListHandler : BaseHandler<GetLocationsListHandler>, IRequestHandler<GetLocationsListQuery, List<LocationDto>>
{
    public GetLocationsListHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<List<LocationDto>> Handle(GetLocationsListQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.LocationRepository.GetLocationsList(request.Level, request.ParentId, request.Status);
        return Mapper.Map<List<LocationDto>>(result.Entities);
    }
}