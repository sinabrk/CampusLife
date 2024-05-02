namespace BG.CampusLife.Presentation.Controllers;

public class UniversityController : BaseController<UniversityController>
{
    /// <summary>
    /// Get List of Universities
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UniversityDto>>> GetList([FromQuery]GetUniversitiesListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get University By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UniversityDto>>> GetById([FromQuery]GetUniversityByIdQuery query) => Ok(await Mediator.Send(query));
    
    
    /// <summary>
    /// Insert / Update University Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UniversityDto>> Upsert([FromBody]UpsertUniversityCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete University Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeleteUniversityCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
