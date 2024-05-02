namespace BG.CampusLife.Application.Locations.Queries.GetByLongLat;

public class GetLocationByLongLatQuery : IRequest<LocationDto>
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}