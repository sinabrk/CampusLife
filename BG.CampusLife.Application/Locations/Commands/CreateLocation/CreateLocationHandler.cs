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

namespace BG.CampusLife.Application.Locations.Commands.CreateLocation
{
    public class CreateLocationHandler : BaseHandler<CreateLocationHandler>, IRequestHandler<CreateLocationCommand, CreateLocationDto>
    {
        private readonly ILocationRepository _repo;

        public CreateLocationHandler(ILogger<CreateLocationHandler> logger, IMapper mapper, ILocationRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<CreateLocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Create(new Domain.Entities.Location()
            {
                Id = request.Id,
                Country = request.Country,
                City = request.City,
                State = request.State,
                Longitude = request.Longitude != 0 ? request.Longitude : 0,
                Latitude = request.Latitude != 0 ? request.Latitude : 0
            });

            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Message);
                throw new BadRequestException(result.Message);
            }

            var location = Mapper.Map<CreateLocationDto>(result.Entity);
            location.FullAddress = string.Join(", ", request.Street, request.DoorNumber, request.PostalCode, request.State, request.City, request.Country);

            return location;
        }
    }
}
