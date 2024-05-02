namespace BG.CampusLife.Presentation.Controllers;

public class IdentityController : BaseController<IdentityController>
{
    /// <summary>
    /// Login API
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginData>> Login(LoginCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Refresh Token API
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginData>> RefreshToken(RefreshTokenCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Register API
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    public async Task<ActionResult> DeactivateAccount() => Ok(await Mediator.Send(new DeactivateAccountCommand()));
}