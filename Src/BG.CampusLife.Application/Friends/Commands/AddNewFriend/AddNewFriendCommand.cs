using System;
using MediatR;

namespace BG.CampusLife.Application.Friends.Commands.AddNewFriend
{
    public class AddNewFriendCommand : IRequest
    {
        public Guid FriendId { get; set; }
    }
}
