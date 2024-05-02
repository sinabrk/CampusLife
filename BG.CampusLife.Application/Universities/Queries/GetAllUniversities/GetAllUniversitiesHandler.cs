using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Queries.DTO;

namespace BG.CampusLife.Application.Universities.Queries.GetAllUniversities
{
    public class GetAllUniversitiesHandler : BaseHandler<GetAllUniversitiesHandler>, IRequestHandler<GetAllUniversitiesQuery, List<UniQueriesDto>>
    {
        private readonly IUniversityRepository _repo;

        public GetAllUniversitiesHandler(IMapper mapper, IUniversityRepository repo, ILogger<GetAllUniversitiesHandler> logger) : base(logger, mapper)
        {
            _repo = repo;
        }

        async Task<List<UniQueriesDto>> IRequestHandler<GetAllUniversitiesQuery, List<UniQueriesDto>>.Handle(GetAllUniversitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAll();
            return Mapper.Map<List<UniQueriesDto>>(result.Entities);
        }
    }
}
