namespace BG.CampusLife.Infrastructure.Notifications;

public class NotificationHub : Hub
{
    private readonly IRepositories _repos;

    public NotificationHub(IRepositories repos)
    {
        _repos = repos;
    }

    public async Task AddToGroup(string user, string groupName)
    {
        await Groups.AddToGroupAsync(user, groupName);
    }

    public async Task RemoveFromGroup(string user, string groupName)
    {
        await Groups.RemoveFromGroupAsync(user, groupName);
    }
    
    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Claims.ToList();
        if (userName != null)
        {
            await _repos.UserManagerServices.UpdateUserConnectionId(userName.Find(i => i.Type == "UserName")?.Value,
                Context.ConnectionId);
            await AddToGroup(Context.ConnectionId, userName.Find(i => i.Type == "UserName")?.Value);
        }

        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userName = Context.User?.Claims.ToList();

        if (userName != null)
            await RemoveFromGroup(Context.ConnectionId, userName.Find(i => i.Type == "UserName")?.Value);
        await base.OnDisconnectedAsync(exception);
    }
}