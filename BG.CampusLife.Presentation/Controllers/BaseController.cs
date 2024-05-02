using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IMediator _mediator;

        public BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ILogger<T> Logger { get; init; }
    }
}
