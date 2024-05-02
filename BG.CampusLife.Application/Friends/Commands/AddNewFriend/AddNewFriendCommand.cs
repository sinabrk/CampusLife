using System;
using BG.CampusLife.Domain.Entities;
using MediatR;

namespace BG.CampusLife.Application.Friends.Commands.AddNewFriend
{
    public class AddNewFriendCommand : IRequest
    {
        public Guid FriendId { get; set; }
    }
}
