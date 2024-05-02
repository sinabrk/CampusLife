namespace BG.CampusLife.Presentation.Controllers;

public class CalendarController : BaseController<CalendarController>
{

    /// <summary>
    /// Get List of Calendars (Has Time Filter)
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CalendarDto>>> GetList([FromQuery]GetCalendarListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Insert / Update Calendar Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CalendarDto>> Upsert([FromBody]UpsertCalendarCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Calendar Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeleteCalendarCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Share Calendar to a user
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> ShareCalendar([FromQuery]ShareCalendarCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    /// <summary>
    /// Revoke Shared Calendar from a user
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UnShareCalendar([FromQuery]UnShareCalendarCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Get Shared Users
    /// </summary>
    /// <returns></returns>
    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SharedCalendarDto>>> GetSharedUsers() =>
        Ok(await Mediator.Send(new GetSharedCalendarUsersQuery()));
    
    /// <summary>
    /// Get Shared Users
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CalendarDto>>> GetSharedCalendars([FromQuery] GetSharedCalendarQuery query) =>
        Ok(await Mediator.Send(query));

}