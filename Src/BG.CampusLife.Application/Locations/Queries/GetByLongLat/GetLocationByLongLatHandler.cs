namespace BG.CampusLife.Application.Locations.Queries.GetByLongLat;

public class GetLocationByLongLatHandler : BaseHandler<GetLocationByLongLatHandler>, IRequestHandler<GetLocationByLongLatQuery, LocationDto>
{
    public GetLocationByLongLatHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<LocationDto> Handle(GetLocationByLongLatQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.LocationRepository.GetLocationByLongLat(longitude: request.Longitude, latitude: request.Latitude);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<LocationDto>(result.Entity);
    }
}