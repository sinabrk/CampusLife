using MediatR;
using System;
using BG.CampusLife.Application.Locations.Commands.DTOs;

namespace BG.CampusLife.Application.Locations.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<CreateLocationDto>
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public string DoorNumber { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
