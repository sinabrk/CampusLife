using BG.CampusLife.Application.Posts.Commands.CreateNewPost;
using BG.CampusLife.Application.Posts.Commands.DeletePost;
using BG.CampusLife.Application.Posts.Commands.UpdatePost;
using BG.CampusLife.Application.Posts.DTOs;
using BG.CampusLife.Application.Posts.Queries.GetPost.ById;
using BG.CampusLife.Application.Posts.Queries.GetPost.ByMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Posts.Queries.AllPosts;
using BG.CampusLife.Application.Posts.Queries.AllUserPosts;
using Microsoft.AspNetCore.Authorization;

namespace BG.CampusLife.Presentation.Controllers
{
    public class PostController : BaseController<PostController>
    {
        public PostController(ILogger<PostController> logger) : base(logger) { }

        #region API's
        #region Commands
        /// <summary>
        ///  Create new Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Post which just created</returns>
        [HttpPost,Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateOrUpdatePostDto>> CreateNewPost(CreatePostCommand post) => Ok(await Mediator.Send(post));

        /// <summary>
        /// Update an existing post.
        /// </summary>
        /// <param name="query"></param>
        /// <returns> The updated post</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateOrUpdatePostDto>> UpdatePost([FromQuery] UpdatePostCommand query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// Delete a specific post with the given id
        /// </summary>
        /// <param name="command"></param>
        /// <returns> status 200 if it works correctly</returns>
        [HttpDelete,Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePost(DeletePostCommand command) => Ok(await Mediator.Send(command));

        #endregion

        #region Queries
        /// <summary>
        /// return post with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPostDto>> GetPostById([FromQuery] GetPostByIdQuery id) => Ok(await Mediator.Send(id));

        /// <summary>
        /// return post with the specific message
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Domain.Entities.Post>>> GetPostByMessage([FromQuery] GetPostByMessageQuery post) => Ok(await Mediator.Send(post));

        /// <summary>
        /// Return all the posts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Domain.Entities.Post>>> GetAllPosts([FromQuery] GetAllPostsQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// Get all the post, posted by specific user
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Domain.Entities.Post>>> GetAllUserPosts([FromQuery] GetAllUserPostsQuery posts) => Ok(await Mediator.Send(posts));
        #endregion
        #endregion
    }
}
