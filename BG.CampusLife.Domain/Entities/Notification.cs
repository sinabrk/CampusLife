using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; } = new();

        [MaxLength(255), Required]
        public string Title { get; set; }
        // Type
        [Required]
        public string Body { get; set; }
        [Required]
        public bool Visited { get; set; } = false;
        [Required]
        public DateTime SendDate { get; set; }
        public DateTime? SeenDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}