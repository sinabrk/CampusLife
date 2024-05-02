using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BG.CampusLife.Domain.Entities
{
    public class University
    {
        public Guid Id { get; set; } = new();

        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public List<User> Users { get; set; }
        
        public Guid LocationId { get; set; }
        [JsonIgnore]
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

    }
}
