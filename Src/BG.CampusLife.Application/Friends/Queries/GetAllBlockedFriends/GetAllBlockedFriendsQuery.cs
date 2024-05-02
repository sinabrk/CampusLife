using System;
using System.Collections.Generic;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Friends.Queries.GetAllBlockedFriends
{
    public class GetAllBlockedFriendsQuery : IRequest<List<FriendDto>>
    {
        public Guid UserId { get; set; }
    }
}
