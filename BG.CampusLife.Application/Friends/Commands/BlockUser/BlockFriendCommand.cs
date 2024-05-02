using System;
using BG.CampusLife.Domain.Entities;
using MediatR;

namespace BG.CampusLife.Application.Friends.Commands.BlockUser
{
    public class BlockFriendCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid FriendsId { get; set; }
    }
}
