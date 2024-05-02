using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Locations.Queries.DTOs;

namespace BG.CampusLife.Application.Locations.Queries.GetAll
{
    public class GetAllLocationHandler : BaseHandler<GetAllLocationHandler>, IRequestHandler<GetAllLocationQuery, List<Domain.Entities.Location>>
    {
        private readonly ILocationRepository _repo;

        public GetAllLocationHandler(ILogger<GetAllLocationHandler> logger, IMapper mapper, ILocationRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<List<Domain.Entities.Location>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAll();
            return result.Entities;
        }
    }
}
