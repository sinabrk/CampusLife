namespace BG.CampusLife.Presentation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController<T> : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
