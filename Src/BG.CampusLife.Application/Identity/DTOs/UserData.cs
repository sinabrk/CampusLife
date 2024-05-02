namespace BG.CampusLife.Application.Identity.DTOs;

public class UserData
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool Suspended { get; set; }
    public bool IsActive { get; set; }
    public string ConnectionId { get; set; }
}