namespace BG.CampusLife.Application.Locations.Commands.UpsertLocation;

public class UpsertLocationHandler : BaseHandler<UpsertLocationHandler>, IRequestHandler<UpsertLocationCommand, LocationDto>
{
    public UpsertLocationHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<LocationDto> Handle(UpsertLocationCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.LocationRepository.CreateOrUpdateLocation(new Location()
        {
            Id = request.Id,
            Title = request.Title,
            Level = request.Level,
            ParentId = request.ParentId,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            Status = request.Status,
        }, cancellationToken);

        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<LocationDto>(result.Entity);
    }
}