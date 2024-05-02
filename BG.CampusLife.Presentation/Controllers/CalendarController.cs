using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Calendars.Commands.DeleteCalendar;
using BG.CampusLife.Application.Calendars.Commands.ShareCalendar;
using BG.CampusLife.Application.Calendars.Commands.UnShareCalendar;
using BG.CampusLife.Application.Calendars.Commands.UpsertCalendar;
using BG.CampusLife.Application.Calendars.DTOs;
using BG.CampusLife.Application.Calendars.Queries.GetList;
using BG.CampusLife.Application.Calendars.Queries.GetSharedCalendar;
using BG.CampusLife.Application.Calendars.Queries.GetSharedUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Presentation.Controllers
{
    public class CalendarController : BaseController<CalendarController>
    {
        public CalendarController(ILogger<CalendarController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get List Of Calendars (Has Time Filter)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CalendarDto>>> GetList([FromQuery]GetCalendarListQuery query) => Ok(await Mediator.Send(query));
        
        /// <summary>
        /// Insert / Update Calendar Entity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CalendarDto>> Upsert([FromBody]UpsertCalendarCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Delete Calendar Entity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete, Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SharedCalendarDto>>> GetSharedUsers() =>
            Ok(await Mediator.Send(new GetSharedCalendarUsersQuery()));
        
        /// <summary>
        /// Get Shared Users
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CalendarDto>>> GetSharedCalendars([FromQuery] GetSharedCalendarQuery query) =>
            Ok(await Mediator.Send(query));

    }
}