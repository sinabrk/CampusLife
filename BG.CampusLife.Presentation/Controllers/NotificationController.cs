using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Notifications.Commands.SendNotification;
using BG.CampusLife.Application.Notifications.Commands.SetNotificationVisited;
using BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail;
using BG.CampusLife.Application.Notifications.Queries.GetUserNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Presentation.Controllers
{
    public class NotificationController : BaseController<NotificationController>
    {
        public NotificationController(ILogger<NotificationController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Send Notification To All Users
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendNotification(SendNotificationCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Set Notification visited
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NotificationVisited(SetNotificationVisitedCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Get user Notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserNotificationDto>>> GetNotifications() => Ok(await Mediator.Send(new GetUserNotificationsQuery()));

        /// <summary>
        /// Get notification details
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<NotificationDetailDto>> GetNotification([FromQuery] GetNotificationDetailQuery query) =>
            Ok(await Mediator.Send(query));
    }
}