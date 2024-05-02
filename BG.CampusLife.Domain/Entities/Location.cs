using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; } = new();

        //[MaxLength(150)]
        //public string? Country { get; set; }

        //[MaxLength(150)]
        //public string? City { get; set; }
        
        //[MaxLength(150)]
        //public string? State { get; set; }
        
        //[MaxLength(255)]
        //public string? Street { get; set; }

        //[MaxLength(30)]
        //public string? DoorNumber { get; set; } = null;

        //[MaxLength(30)]
        //public string? PostalCode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<User> Users { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<University> Universities { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<Post> Posts { get; set; }
    }
}