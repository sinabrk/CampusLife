namespace BG.CampusLife.Application.Universities.DTOs;

public class UniversityDto : IMapFrom<University>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }
    public bool Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, UniversityDto>()
            .ForMember(cld => cld.LocationName,
                opt =>
                    opt.MapFrom(c => c.Location.Title));
    }
}