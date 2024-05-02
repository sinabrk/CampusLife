using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class ConversationChat
    {
        public Guid Id { get; set; } = new();
        
        public Guid ConversationId { get; set; }
        [JsonIgnore]
        public Conversation Conversation { get; set; }

        public Guid SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [MaxLength]
        public string Message { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public bool Seen { get; set; }
        public DateTime? SeenDate { get; set; }

        [MaxLength(50)]        
        public string MimeType { get; set; }
        [MaxLength]
        public string Attachment { get; set; }

        public int ChatOrder { get; set; }
    }
}
