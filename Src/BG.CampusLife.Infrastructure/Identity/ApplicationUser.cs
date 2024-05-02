namespace BG.CampusLife.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public bool IsSuspend { get; set; }
    public bool IsActive { get; set; }
    public string ConnectionId { get; set; }
    public DateTime LastDeactivate { get; set; }
}