namespace BG.CampusLife.Application.Locations.DTOs;

public class LocationDto : IMapFrom<Location>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Level { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentTitle { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public bool Status { get; set; }
    // this will only return one level below not all children
    public List<LocationDto> Children { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Location, LocationDto>()
        .ForMember(dto => dto.ParentTitle,
            opt => opt.MapFrom(
                    ent => ent.Parent.Title)
            );
    }
}