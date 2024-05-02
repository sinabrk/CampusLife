using System;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendByEmail
{
    public class GetFriendByEmailQuery : IRequest<FriendDto>
    {
        public string Email { get; set; }
    }
}
