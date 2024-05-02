using System;
using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Application.Friends.Queries.DTOs;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsByName
{
    public class GetFriendByNameQuery : IRequest<List<FriendDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
