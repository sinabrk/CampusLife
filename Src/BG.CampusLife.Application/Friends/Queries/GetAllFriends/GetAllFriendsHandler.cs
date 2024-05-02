// using AutoMapper;
// using BG.CampusLife.Application.Common;
// using BG.CampusLife.Application.Interfaces.Repositories;
// using MediatR;
// using Microsoft.Extensions.Logging;
// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using BG.CampusLife.Application.Friends.Queries.DTOs;
// using BG.CampusLife.Application.Interfaces.Services;
// using BG.CampusLife.Domain.Enums;
// using BG.CampusLife.Domain.Exceptions;
//
// namespace BG.CampusLife.Application.Friends.Queries.GetAllFriends
// {
//     public class GetAllFriendsHandler : BaseHandler<GetAllFriendsHandler>, IRequestHandler<GetAllFriendsQuery, List<FriendDto>>
//     {
//         private readonly IFriendRepository _friendRepository;
//         private readonly ICurrentUserService _currentUser;
//         private readonly IUserRepository _userRepository;
//
//         public GetAllFriendsHandler(ILogger<GetAllFriendsHandler> logger, IMapper mapper, IFriendRepository friendRepository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
//         {
//             _friendRepository = friendRepository;
//             _currentUser = currentUser;
//             _userRepository = userRepository;
//         }
//
//         public async Task<List<FriendDto>> Handle(GetAllFriendsQuery request, CancellationToken cancellationToken)
//         {
//
//             var user = await _userRepository.GetById(_currentUser.UserId);
//             if (!user.Succeeded)
//                 throw new NotFoundException(user.Message);           
//             
//             var result = await _friendRepository.GetAllFriends(user.Entity);
//             if (!result.Succeeded)
//                 throw new NotFoundException(result.Message);
//             return Mapper.Map<List<FriendDto>>(result.Entities);
//         }
//     }
// }
