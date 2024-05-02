using System;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsById
{
    public class GetFriendByIdQuery : IRequest<FriendDto>
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
    }
}
