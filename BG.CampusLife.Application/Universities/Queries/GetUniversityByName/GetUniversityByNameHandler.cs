using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Queries.DTO;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityByName
{
    public class GetUniversityByNameHandler : BaseHandler<GetUniversityByNameHandler>,
        IRequestHandler<GetUniversityByNameQuery, UniQueriesDto>
    {
        private readonly IUniversityRepository _repo;

        public GetUniversityByNameHandler(IUniversityRepository repo, IMapper mapper,
            ILogger<GetUniversityByNameHandler> logger) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<UniQueriesDto> Handle(GetUniversityByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetUniversityByName(request.Name);

            if (!result.Succeeded)
            {
                Logger.LogError(result.Message);
                throw new NotFoundException(result.Message);
            }

            Logger.LogInformation(
                $"University with the following data has been found: {result.Entity.Name},  {result.Entity.Location}");

            return Mapper.Map<UniQueriesDto>(result.Entity);
        }
    }
}