// using System.Linq;
// using AutoMapper;
// using BG.CampusLife.Application.Common;
// using BG.CampusLife.Application.Interfaces.Repositories;
// using BG.CampusLife.Domain.Entities;
// using BG.CampusLife.Domain.Exceptions;
// using MediatR;
// using Microsoft.Extensions.Logging;
// using System.Threading;
// using System.Threading.Tasks;
// using BG.CampusLife.Application.Interfaces.Services;
//
// namespace BG.CampusLife.Application.Friends.Commands.AddNewFriend
// {
//     public class AddNewFriendHandler : BaseHandler<AddNewFriendHandler>, IRequestHandler<AddNewFriendCommand>
//     {
//         private readonly IFriendRepository _friendRepository;
//         private readonly ICurrentUserService _currentUser;
//         private readonly IUserRepository _userRepository;
//
//         public AddNewFriendHandler(ILogger<AddNewFriendHandler> logger, IMapper mapper, IFriendRepository friendRepository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
//         {
//             _friendRepository = friendRepository;
//             _currentUser = currentUser;
//             _userRepository = userRepository;
//         }
//
//         public async Task<Unit> Handle(AddNewFriendCommand request, CancellationToken cancellationToken)
//         {
//             var user = await _userRepository.GetById(_currentUser.UserId);
//             if (!user.Succeeded) throw new NotFoundException(user.Message);
//             
//             var result = await _friendRepository.AddNewFriend(user.Entity, request.FriendId);
//             if (result.Succeeded) return Unit.Value;
//             Logger.LogWarning("Error: " + result.Message);
//             throw new NotFoundException(result.Message);
//         }
//     }
// }
