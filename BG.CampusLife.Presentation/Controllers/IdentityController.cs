using BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;
using BG.CampusLife.Application.Identity.Commands.Deactivate;
using BG.CampusLife.Application.Identity.Commands.ForgetPassword;
using BG.CampusLife.Application.Identity.Commands.Login;
using BG.CampusLife.Application.Identity.Commands.RefreshToken;
using BG.CampusLife.Application.Identity.Commands.Register;
using BG.CampusLife.Application.Identity.Commands.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BG.CampusLife.Application.Identity.Commands.UpdateProfile;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Identity.Queries.GetUserProfile;
using Microsoft.AspNetCore.Http;

namespace BG.CampusLife.Presentation.Controllers
{
    public class IdentityController : BaseController<IdentityController>
    {
        public IdentityController(ILogger<IdentityController> logger) : base(logger) { }

        /// <summary>
        /// Login API
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<LoginData>> Login(LoginCommand command) => Ok(await Mediator.Send(command));


        /// <summary>
        /// Refresh Token API
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<LoginData>> RefreshToken(RefreshTokenCommand command) => Ok(await Mediator.Send(command));


        /// <summary>
        /// Register API
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Forget Password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Confirm Email Token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> ConfirmEmailToken([FromQuery] ConfirmEmailTokenCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// User Account Deactivation
        /// </summary>
        /// <returns></returns>
        [HttpPatch, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeactivateAccount() => Ok(await Mediator.Send(new DeactivateAccountCommand()));

        

    }
}
