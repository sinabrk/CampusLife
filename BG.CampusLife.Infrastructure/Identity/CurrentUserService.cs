using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Infrastructure.Persistence;

namespace BG.CampusLife.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserName");
            ConnectionId = httpContextAccessor.HttpContext?.User?.FindFirstValue("ConnectionId");

            IsAuthenticated = UserId != null;
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public string ConnectionId { get; set; }
    }
}