using System;
using System.Linq;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace BG.CampusLife.Infrastructure.Notifications
{
    public class NotificationHub : Hub
    {
        private readonly IUserManager _userManager;

        public NotificationHub(IUserManager userManager)
        {
            _userManager = userManager;
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
                await _userManager.UpdateUserConnectionId(userName.Find(i => i.Type == "UserName")?.Value,
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
}