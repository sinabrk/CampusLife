using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BG.CampusLife.Domain.Entities
{
    /// <summary>
    /// this class is the selected values of property in the property entity
    /// the important part is value
    /// </summary>
    public class UserProperty
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid PropertyId { get; set; }
        [JsonIgnore, ForeignKey("PropertyId")]
        public Property Property { get; set; }
        
        [MaxLength(255)]
        public string Value { get; set; }
    }
}