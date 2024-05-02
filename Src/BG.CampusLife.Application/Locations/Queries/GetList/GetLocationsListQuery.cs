namespace BG.CampusLife.Application.Locations.Queries.GetList;

public class GetLocationsListQuery : IRequest<List<LocationDto>>
{
    public int? Level { get; set; }
    public Guid? ParentId { get; set; }
    [DefaultValue(true)]
    public bool Status { get; set; }
}