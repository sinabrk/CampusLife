using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Queries.DTO;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityByLocation
{
    public class GetUniversityByLocationHandler : BaseHandler<GetUniversityByLocationHandler>,
        IRequestHandler<GetUniversityByLocationQuery, List<UniQueriesDto>>
    {
        private readonly IUniversityRepository _repo;

        public GetUniversityByLocationHandler(IUniversityRepository repo,
            ILogger<GetUniversityByLocationHandler> logger, IMapper mapper) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<List<UniQueriesDto>> Handle(GetUniversityByLocationQuery request,
            CancellationToken cancellationToken)
        {
            var result =
                await _repo.GetUniversityByLocation(request.UniverstiyLocation.Country,
                    request.UniverstiyLocation.City);
            return Mapper.Map<List<UniQueriesDto>>(result.Entities);
        }
    }
}