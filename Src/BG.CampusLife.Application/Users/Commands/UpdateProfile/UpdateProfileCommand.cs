namespace BG.CampusLife.Application.Users.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest
{
    public Guid? UniversityId { get; set; }
    public Guid? LocationId { get; set; }
    public bool Private { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string Bio { get; set; }
    public byte MarriageStatus { get; set; }
    public DateTime? Started { get; set; }
    public DateTime? Graduation { get; set; }
    public bool Graduated { get; set; }
    public string Title { get; set; }
    public string PersonalEmail { get; set; }
    public string AdditionalEmail { get; set; }
}