using System;

namespace BG.CampusLife.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        string ConnectionId { get; }
    }
}