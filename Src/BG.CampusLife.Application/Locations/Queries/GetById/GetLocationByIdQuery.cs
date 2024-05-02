namespace BG.CampusLife.Application.Locations.Queries.GetById;

public class GetLocationByIdQuery : IRequest<LocationDto>
{
    public Guid Id { get; set; }
}