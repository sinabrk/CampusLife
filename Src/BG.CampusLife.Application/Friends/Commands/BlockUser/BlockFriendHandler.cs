// using AutoMapper;
// using BG.CampusLife.Application.Common;
// using BG.CampusLife.Application.Interfaces.Repositories;
// using BG.CampusLife.Domain.Exceptions;
// using MediatR;
// using Microsoft.Extensions.Logging;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace BG.CampusLife.Application.Friends.Commands.BlockUser
// {
//     public class BlockFriendHandler : BaseHandler<BlockFriendHandler>, IRequestHandler<BlockFriendCommand>
//     {
//         private readonly IFriendRepository _repo;
//
//         public BlockFriendHandler(ILogger<BlockFriendHandler> logger, IMapper mapper, IFriendRepository repo) : base(logger, mapper)
//         {
//             _repo = repo;
//         }
//
//         public async Task<Unit> Handle(BlockFriendCommand request, CancellationToken cancellationToken)
//         {
//             try
//             {
//                 // await _repo.BlockFriend(request.UserId, request.FriendsId);
//
//                 return Unit.Value;
//             }
//             catch (NotFoundException ex)
//             {
//                 Logger.LogWarning(ex.ToString());
//                 throw;
//             }
//             catch (Exception ex)
//             {
//                 Logger.LogError(ex.ToString());
//                 throw;
//             }
//         }
//     }
// }
