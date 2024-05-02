namespace BG.CampusLife.Presentation.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
        UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserName");
        ConnectionId = httpContextAccessor.HttpContext?.User?.FindFirstValue("ConnectionId");
        Role = httpContextAccessor.HttpContext?.User?.FindFirstValue("Role");

        IsAuthenticated = UserId != null;
    }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public bool IsAuthenticated { get; set; }

    public string ConnectionId { get; set; }

    public string Role { get; set; }
}