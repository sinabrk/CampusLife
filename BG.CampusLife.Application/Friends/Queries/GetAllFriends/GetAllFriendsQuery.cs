using System;
using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Application.Friends.Queries.DTOs;

namespace BG.CampusLife.Application.Friends.Queries.GetAllFriends
{
    public class GetAllFriendsQuery : IRequest<List<FriendDto>>
    {
    }
}
