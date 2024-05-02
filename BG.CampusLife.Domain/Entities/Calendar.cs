using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    /// <summary>
    /// In this entity only we save user prefered dates and events
    /// not the whole calendar
    /// event is optional so user can use event freely
    /// </summary>
    public class Calendar
    {
        public Guid Id { get; set; } = new();

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid EntityId { get; set; }
        public EntityTypes EntityType { get; set; }
        
        
    }
}