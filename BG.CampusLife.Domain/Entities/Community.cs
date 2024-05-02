using BG.CampusLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    public class Community
    {
        public Guid Id { get; set; } = new();

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        public PrivacyType PrivacyModus { get; set; } = PrivacyType.Public;

#nullable enable
        [Column("UserAdminId")]
        public List<string>? AdminId { get; set; }
#nullable disable
        
        public List<User> Members { get; set; }

        [JsonIgnore]
        public List<Post> Posts { get; set; }
    }
}
