using AutoMapper;
using BG.CampusLife.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Common
{
    public class BaseRepository<Y, T>
    {
        public BaseRepository(Y context, ILogger<T> logger, IMapper mapper)
        {
            Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        protected Y Context { get; init; }
        protected ILogger<T> Logger { get; init; }
        protected IMapper Mapper { get; set; }
    }
}
