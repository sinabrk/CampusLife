namespace BG.CampusLife.Application.Locations.Queries.GetById;

public class GetLocationByIdHandler : BaseHandler<GetLocationByIdHandler>, IRequestHandler<GetLocationByIdQuery, LocationDto>
{
    public GetLocationByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<LocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.LocationRepository.GetLocationById(request.Id);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<LocationDto>(result.Entity);
    }
}