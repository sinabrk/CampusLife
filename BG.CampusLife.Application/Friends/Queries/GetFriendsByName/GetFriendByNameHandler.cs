using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Friends.Queries.GetFriendsByName;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsByName
{
    class GetFriendByNameHandler : BaseHandler<GetFriendByNameHandler>, IRequestHandler<GetFriendByNameQuery, List<FriendDto>>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;
        public GetFriendByNameHandler(ILogger<GetFriendByNameHandler> logger, IMapper mapper, IFriendRepository friendRepository, IUserRepository userRepository, ICurrentUserService currentUser) : base(logger, mapper)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<List<FriendDto>> Handle(GetFriendByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _friendRepository.GetFriendByName(user.Entity, request.FirstName, request.LastName);
            if (result.Succeeded) Mapper.Map<List<FriendDto>>(result.Entities);
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}
