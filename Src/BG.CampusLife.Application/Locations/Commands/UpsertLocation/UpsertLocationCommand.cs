namespace BG.CampusLife.Application.Locations.Commands.UpsertLocation;

public class UpsertLocationCommand : IRequest<LocationDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Level { get; set; }
    public Guid? ParentId { get; set; }
    public double Longitude { get; set; }
    
    public double Latitude { get; set; }
    public bool Status { get; set; }
}