using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; } = new();

        public Guid EntityId { get; set; }
        public EntityTypes EntityType { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        
        [MaxLength(500)]
        public string MimeType { get; set; }

        [MaxLength(20)]
        public string Extension { get; set; }
        
        [MaxLength(500)]
        public string FileName { get; set; }

        [MaxLength(500)]
        public string FilePath { get; set; }
        
        public DateTime Created { get; set; }

    }
}
