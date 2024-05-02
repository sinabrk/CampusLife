namespace BG.CampusLife.Presentation.Controllers;

[Authorize]
public class DocumentController : BaseController<DocumentController>
{
    /// <summary>
    /// Upload Document
    /// </summary>
    /// <param name="command"></param>
    /// <returns>
    /// Status Code 200
    /// </returns>
    [HttpPost, Authorize]
    [ProducesResponseType(typeof(UploadDocumentDto), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<UploadDocumentDto>> Upload([FromForm]UploadDocumentCommand command) =>
        Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete User Uploaded Document
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <response code="403">If that is not your file</response>
    /// <response code="404">Not Found</response>
    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromQuery] DeleteDocumentCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}