namespace BG.CampusLife.Presentation.Controllers;

public class LocationController : BaseController<LocationController>
{
    /// <summary>
    /// Get List of Locations
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LocationDto>>> GetList([FromQuery] GetLocationsListQuery query) => Ok(await Mediator.Send(query));

    /// <summary>
    /// Get Location By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LocationDto>>> GetById([FromQuery] GetLocationByIdQuery query) => Ok(await Mediator.Send(query));

    /// <summary>
    /// Get Location By Longitude and Latitude
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LocationDto>>> GetByLongLat([FromQuery] GetLocationByLongLatQuery query) => Ok(await Mediator.Send(query));

    /// <summary>
    /// Insert / Update Location Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<LocationDto>> Upsert([FromBody] UpsertLocationCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Location Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery] DeleteLocationCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
