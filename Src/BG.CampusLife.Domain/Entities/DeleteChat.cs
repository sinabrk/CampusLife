namespace BG.CampusLife.Domain.Entities;

public class DeleteChat : BaseEntity
{
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public Guid ChatId { get; set; }
    [JsonIgnore]
    public ConversationChat ConversationChat { get; set; }
}
