using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Locations.Commands.DTOs;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Locations.Commands.UpdateLocation
{
    public class UpdateLocationHandler : BaseHandler<UpdateLocationHandler>, IRequestHandler<UpdateLocationCommand, UpdateLocationDto>
    {
        private readonly ILocationRepository _repo;

        public UpdateLocationHandler(ILogger<UpdateLocationHandler> logger, IMapper mapper, ILocationRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<UpdateLocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Update(Mapper.Map<Domain.Entities.Location>(request));
            if (result.Succeeded) return Mapper.Map<UpdateLocationDto>(result.Entity);
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}
