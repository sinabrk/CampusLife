using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BG.CampusLife.Domain.Entities
{
    public class Blog
    {
        public Guid Id { get; set; } = new();

        [MaxLength]
        [Required]
        public string Description { get; set; }

        [MaxLength(150)]
        public string Summary { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId)")]
        public User User { get; set; }

        public List<Post> Posts { get; set; }
    }
}
