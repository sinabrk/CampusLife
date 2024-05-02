using MediatR;
using System;
using BG.CampusLife.Application.Locations.Queries.DTOs;

namespace BG.CampusLife.Application.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IRequest<GetLocationDto>
    {
        public Guid Id { get; set; }
    }
}
