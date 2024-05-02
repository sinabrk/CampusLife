using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class ConversationMember
    {
        public Guid ConversationId { get; set; }
        [JsonIgnore]
        public Conversation Conversation { get; set; }

        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public User Member { get; set; }

        public DateTime Created { get; set; }
    }
}