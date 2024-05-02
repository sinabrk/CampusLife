namespace BG.CampusLife.Presentation.Controllers;

public class TagController : BaseController<TagController>
{
    /// <summary>
    /// Get List of Tags
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TagListDto>>> GetList([FromQuery]GetTagsListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get Tag By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TagDto>>> GetById([FromQuery]GetTagByIdQuery query) => Ok(await Mediator.Send(query));

    
    /// <summary>
    /// Insert / Update Tag Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UpsertTagDto>> Upsert([FromBody]UpsertTagCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Tag Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeleteTagCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

}