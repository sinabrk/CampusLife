namespace BG.CampusLife.Presentation.Controllers;

public class PostController : BaseController<PostController>
{
    /// <summary>
    /// Get List of Posts
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PostDto>>> GetList([FromQuery]GetPostsListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get Post By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PostDto>>> GetById([FromQuery]GetPostByIdQuery query) => Ok(await Mediator.Send(query));
    
    
    /// <summary>
    /// Insert / Update Post Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PostDto>> Upsert([FromBody]UpsertPostCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Post Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeletePostCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}