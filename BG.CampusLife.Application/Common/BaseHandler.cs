using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Common
{
    public class BaseHandler<T>
    {
        public BaseHandler(ILogger<T> logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        protected ILogger<T> Logger { get; init; }
        protected IMapper Mapper { get; init; }
    }
}
