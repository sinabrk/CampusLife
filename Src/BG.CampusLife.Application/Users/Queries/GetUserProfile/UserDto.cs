namespace BG.CampusLife.Application.Users.Queries.GetUserProfile;

public class UserDto : IMapFrom<User>
{
    public Guid Id { get; set; }
    public Guid? UniversityId { get; set; }
    public Guid? LocationId { get; set; }
    public bool Private { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string Bio { get; set; }
    public byte MarriageStatus { get; set; }
    public DateTime? Started { get; set; }
    public DateTime? Graduation { get; set; }
    public bool Graduated { get; set; }
    public string Title { get; set; }
    public string PersonalEmail { get; set; }
    public string AdditionalEmail { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(dto => dto.Gender, opt => opt.MapFrom(ent => ent.Gender.ToString()));
    }
}