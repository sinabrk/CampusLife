using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = new();

        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        
        [MaxLength(500)]
        public string Title { get; set; } = default!;

        public string Body { get; set; } 

        public DateTime Created { get; set; }

        [JsonIgnore]
        public List<Document> Attachments { get; set; } = default!;

        public Guid LocationId { get; set; }

        [JsonIgnore]
        [ForeignKey("LocationId)")]
        public Location Location { get; set; }

        [JsonIgnore]
        public List<Community> Community { get; set; }
    }
}
