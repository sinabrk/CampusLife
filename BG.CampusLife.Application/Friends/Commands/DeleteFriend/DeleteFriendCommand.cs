using System;
using BG.CampusLife.Domain.Entities;
using MediatR;

namespace BG.CampusLife.Application.Friends.Commands.DeleteFriend
{
    public class DeleteFriendCommand : IRequest
    {
        public Guid FriendId { get; set; }
    }
}
