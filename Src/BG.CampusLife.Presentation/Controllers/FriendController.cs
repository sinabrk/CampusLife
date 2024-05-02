// namespace BG.CampusLife.Presentation.Controllers
// {
//     public class FriendController : BaseController<FriendController>
//     {
//         #region Ctor
//         #endregion
//
//         #region API's
//
//         #region Commands
//         /// <summary>
//         /// Adding new friend for specific user
//         /// </summary>
//         /// <param name="command">TargetPerson, Requestor</param>
//         /// <returns></returns>
//         [HttpPost]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult> AddNewFriend(AddNewFriendCommand command) => Ok(await Mediator.Send(command));
//
//         /// <summary>
//         /// Deleting friend from FriendsList
//         /// </summary>
//         /// <param name="command">UserId, TargetPerson</param>
//         /// <returns></returns>
//         [HttpDelete]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult> DeleteFriend(DeleteFriendCommand command) => Ok(await Mediator.Send(command));
//
//         /// <summary>
//         /// Add a friend to blackList
//         /// </summary>
//         /// <param name="command"></param>
//         /// <returns></returns>
//         [HttpPut]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult> BlockFriend(BlockFriendCommand command) => Ok(await Mediator.Send(command));
//         #endregion
//
//         #region Queries
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="query"></param>
//         /// <returns></returns>
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<FriendDto>> GetUserByName([FromQuery] GetFriendByNameQuery query) => Ok(await Mediator.Send(query));
//
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="query"></param>
//         /// <returns></returns>
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<FriendDto>> GetFriendById([FromQuery] GetFriendByIdQuery query) => Ok(await Mediator.Send(query));
//
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="query"></param>
//         /// <returns></returns>
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<List<FriendDto>>> GetAllFriends(GetAllFriendsQuery query) => Ok(await Mediator.Send(query));
//
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<FriendDto>> GetFriendByEmailAddress(GetFriendByEmailQuery query) => Ok(await Mediator.Send(query));
//
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<List<FriendDto>>> GetAllBlockedFriends([FromQuery] GetAllBlockedFriendsQuery query) => Ok(await Mediator.Send(query));
//         #endregion
//
//         #endregion
//
//     }
// }
