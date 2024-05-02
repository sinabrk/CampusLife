using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsById
{
    public class GetFriendByIdHandler : BaseHandler<GetFriendByIdHandler>, IRequestHandler<GetFriendByIdQuery, FriendDto>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;

        public GetFriendByIdHandler(ILogger<GetFriendByIdHandler> logger, IMapper mapper, IFriendRepository friendRepository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _friendRepository = friendRepository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<FriendDto> Handle(GetFriendByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _friendRepository.GetFriendById(user.Entity, request.FriendId);
            if (result.Succeeded) return Mapper.Map<FriendDto>(result.Entity);
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}
