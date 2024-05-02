using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; } = new();

        [MaxLength(250)]
        public string Title { get; set; }
        
        public Guid OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        
        public DateTime Created { get; set; }
    }
}