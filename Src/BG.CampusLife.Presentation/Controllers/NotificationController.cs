namespace BG.CampusLife.Presentation.Controllers;

public class NotificationController : BaseController<NotificationController>
{
    /// <summary>
    /// Send Notification To All Users
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendNotification([FromBody]SendNotificationCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Set Notification visited
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    public async Task<IActionResult> NotificationVisited([FromQuery]SetNotificationVisitedCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Get user Notifications
    /// </summary>
    /// <returns></returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<List<UserNotificationDto>>> GetNotifications() => Ok(await Mediator.Send(new GetUserNotificationsQuery()));

    /// <summary>
    /// Get notification details
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<NotificationDetailDto>> GetNotification([FromQuery] GetNotificationDetailQuery query) =>
        Ok(await Mediator.Send(query));
}