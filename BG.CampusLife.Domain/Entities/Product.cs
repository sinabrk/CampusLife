using BG.CampusLife.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = new();

        [MaxLength(250)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductTypes Type { get; set; }

        public DateTime Created { get; set; }

        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        
        // public Advertise RelatedPost { get; set; }
    }
}
