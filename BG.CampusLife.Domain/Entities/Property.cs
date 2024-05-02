using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    /// <summary>
    /// This class is for all static props that we show to user
    /// Work experiences
    /// Hobbies
    /// sports
    /// and others go to this
    /// with our defined values
    /// options are separated by pipe character in front and all values defined in a single string
    /// </summary>
    public class Property
    {
        public Guid Id { get; set; } = new();
        [MaxLength(50)]
        public string Type { get; set; }

        // Maybe Enum
        [MaxLength(50)]
        public string ControlType { get; set; }
        
        [MaxLength(250)]
        public string Name { get; set; }
        // Separated By Enum in front
        public string Options { get; set; }
        
        [MaxLength(250)]
        [JsonIgnore]
        public string NormalizedKey { get; set; }
        [JsonIgnore]
        public string NormalizedValue { get; set; }
        
    }
}