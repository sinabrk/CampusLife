using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; } = new();
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public DateTime Created { get; set; }
    }
}