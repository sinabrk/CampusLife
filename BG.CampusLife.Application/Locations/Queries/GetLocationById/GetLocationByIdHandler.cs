using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Locations.Queries.DTOs;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Locations.Queries.GetLocationById
{
    public class GetLocationByIdHandler : BaseHandler<GetLocationByIdHandler>,
        IRequestHandler<GetLocationByIdQuery, GetLocationDto>
    {
        private readonly ILocationRepository _repo;

        public GetLocationByIdHandler(ILogger<GetLocationByIdHandler> logger, IMapper mapper, ILocationRepository repo)
            : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<GetLocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetById(request.Id);
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Message);
                throw new NotFoundException(result.Message);
            }

            var location = Mapper.Map<GetLocationDto>(result.Entity);

            location.FullAddress = result.Entity.Street + ", " + result.Entity.DoorNumber + ", " +
                                   result.Entity.PostalCode + ", " + result.Entity.City + ", " + result.Entity.State +
                                   ", " + result.Entity.Country;
            location.Universities =
                Mapper.Map<List<Domain.Entities.University>, List<Domain.Entities.University>>(result.Entity
                    .Universities);
            location.Posts = Mapper.Map<List<Domain.Entities.Post>, List<Domain.Entities.Post>>(result.Entity.Posts);
            location.Users = Mapper.Map<List<Domain.Entities.User>, List<Domain.Entities.User>>(result.Entity.Users);

            return location;
        }
    }
}