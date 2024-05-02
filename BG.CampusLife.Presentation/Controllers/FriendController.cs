using BG.CampusLife.Application.Friends.Commands.AddNewFriend;
using BG.CampusLife.Application.Friends.Commands.BlockUser;
using BG.CampusLife.Application.Friends.Commands.DeleteFriend;
using BG.CampusLife.Application.Friends.Queries.DTOs;
using BG.CampusLife.Application.Friends.Queries.GetAllBlockedFriends;
using BG.CampusLife.Application.Friends.Queries.GetAllFriends;
using BG.CampusLife.Application.Friends.Queries.GetFriendByEmail;
using BG.CampusLife.Application.Friends.Queries.GetFriendsById;
using BG.CampusLife.Application.Friends.Queries.GetFriendsByName;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BG.CampusLife.Presentation.Controllers
{
    public class FriendController : BaseController<FriendController>
    {
        #region Ctor
        public FriendController(ILogger<FriendController> logger) : base(logger) { }
        #endregion

        #region API's

        #region Commands
        /// <summary>
        /// Adding new friend for specific user
        /// </summary>
        /// <param name="command">TargetPerson, Requestor</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddNewFriend(AddNewFriendCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Deleting friend from FriendsList
        /// </summary>
        /// <param name="command">UserId, TargetPerson</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteFriend(DeleteFriendCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Add a friend to blackList
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> BlockFriend(BlockFriendCommand command) => Ok(await Mediator.Send(command));
        #endregion

        #region Queries
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FriendDto>> GetUserByName([FromQuery] GetFriendByNameQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FriendDto>> GetFriendById([FromQuery] GetFriendByIdQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<FriendDto>>> GetAllFriends(GetAllFriendsQuery query) => Ok(await Mediator.Send(query));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FriendDto>> GetFriendByEmailAddress(GetFriendByEmailQuery query) => Ok(await Mediator.Send(query));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<FriendDto>>> GetAllBlockedFriends([FromQuery] GetAllBlockedFriendsQuery query) => Ok(await Mediator.Send(query));
        #endregion

        #endregion

    }
}
