using BG.CampusLife.Domain.Enums;
using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BG.CampusLife.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; } = new();
        
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        public RatingScale Stars { get; set; }
        
        public string ParticipantId { get; set; }
    }
}
