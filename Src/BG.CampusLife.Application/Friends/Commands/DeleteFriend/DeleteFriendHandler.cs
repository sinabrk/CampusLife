// using AutoMapper;
// using BG.CampusLife.Application.Common;
// using BG.CampusLife.Application.Interfaces.Repositories;
// using BG.CampusLife.Domain.Exceptions;
// using MediatR;
// using Microsoft.Extensions.Logging;
// using System;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using BG.CampusLife.Application.Interfaces.Services;
//
// namespace BG.CampusLife.Application.Friends.Commands.DeleteFriend
// {
//     public class DeleteFriendHandler : BaseHandler<DeleteFriendHandler>, IRequestHandler<DeleteFriendCommand>
//     {
//         private readonly IFriendRepository _friendRepository;
//         private readonly ICurrentUserService _currentUser;
//         private readonly IUserRepository _userRepository;
//
//         public DeleteFriendHandler(ILogger<DeleteFriendHandler> logger, IMapper mapper, ICurrentUserService currentUser, IFriendRepository friendRepository, IUserRepository userRepository) : base(
//             logger, mapper)
//         {
//             _currentUser = currentUser;
//             _friendRepository = friendRepository;
//             _userRepository = userRepository;
//         }
//
//         public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
//         {
//             
//             var user = await _userRepository.GetById(_currentUser.UserId);
//             if (!user.Succeeded) throw new NotFoundException(user.Message);
//             
//             var result = await _friendRepository.DeleteFriend(user.Entity, request.FriendId);
//             if (result.Succeeded) return Unit.Value;
//             Logger.LogWarning($"Error: {result.Message}");
//             throw new NotFoundException(result.Message);
//         }
//     }
// }