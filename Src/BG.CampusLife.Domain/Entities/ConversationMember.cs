namespace BG.CampusLife.Domain.Entities;

public class ConversationMember : BaseEntity
{
    public Guid ConversationId { get; set; }
    [JsonIgnore]
    public Conversation Conversation { get; set; }

    public Guid MemberId { get; set; }
    [ForeignKey("MemberId")]
    public User Member { get; set; }

}