using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Friends.Queries.DTOs;

namespace BG.CampusLife.Application.Friends.Queries.GetAllBlockedFriends
{
    public class GetAllBlockedFriendsHandler : BaseHandler<GetAllBlockedFriendsHandler>, IRequestHandler<GetAllBlockedFriendsQuery, List<FriendDto>>
    {
        private readonly IFriendRepository _repo;

        public GetAllBlockedFriendsHandler(ILogger<GetAllBlockedFriendsHandler> logger, IMapper mapper, IFriendRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<List<FriendDto>> Handle(GetAllBlockedFriendsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // return Mapper.Map<List<FriendDto>>(await _repo.GetAllBlockedFriends(request.UserId));
                return Mapper.Map<List<FriendDto>>(new object());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
