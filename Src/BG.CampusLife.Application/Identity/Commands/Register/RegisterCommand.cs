namespace BG.CampusLife.Application.Identity.Commands.Register;

public class RegisterCommand : IRequest
{
    public string Email { get; set; }
    // public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public Roles Role { get; set; }
    public Guid? LocationId { get; set; }
    public Guid? UniversityId { get; set; }
}
