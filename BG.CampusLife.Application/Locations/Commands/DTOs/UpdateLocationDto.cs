using System;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Locations.Commands.DTOs
{
    public class UpdateLocationDto
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public List<Domain.Entities.Post> Posts { get; set; }

        public List<Domain.Entities.User> Users { get; set; }
    }
}
