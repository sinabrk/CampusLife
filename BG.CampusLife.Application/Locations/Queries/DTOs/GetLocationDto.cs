using BG.CampusLife.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Locations.Queries.DTOs
{
    public class GetLocationDto
    {
        public GetLocationDto()
        {
            Users = new List<User>();
            Posts = new List<Domain.Entities.Post>();
            Universities = new List<Domain.Entities.University>();
        }

        public Guid Id { get; set; }

        public string FullAddress { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public List<User> Users { get; set; }
       
        public List<Domain.Entities.Post> Posts { get; set; }

        public List<Domain.Entities.University> Universities { get; set; }
    }
}
