namespace BG.CampusLife.Presentation.Controllers;

public class PropertyController : BaseController<PropertyController>
{
    /// <summary>
    /// Get List of Properties
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PropertyListDto>>> GetList([FromQuery]GetPropertiesListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get Property By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PropertyDto>>> GetById([FromQuery]GetPropertyByIdQuery query) => Ok(await Mediator.Send(query));

    
    /// <summary>
    /// Insert / Update Property Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpsertPropertyDto>> Upsert([FromBody]UpsertPropertyCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Property Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete([FromQuery]DeletePropertyCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

}