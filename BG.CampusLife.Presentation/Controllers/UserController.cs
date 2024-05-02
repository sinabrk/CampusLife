using System.Threading.Tasks;
using BG.CampusLife.Application.Identity.Commands.UpdateProfile;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Identity.Queries.GetUserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Presentation.Controllers
{
    public class UserController : BaseController<UserController>
    {
        public UserController(ILogger<UserController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDto>> GetProfile() => Ok(await Mediator.Send(new GetUserProfileQuery()));

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <returns></returns>
        [HttpPatch, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UpdateProfileCommand command) =>
            Ok(await Mediator.Send(command));
    }
}