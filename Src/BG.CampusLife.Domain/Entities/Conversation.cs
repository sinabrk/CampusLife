namespace BG.CampusLife.Domain.Entities;

public class Conversation : BaseEntity
{
    [MaxLength(250)]
    public string Title { get; set; }
    
    public Guid OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public User Owner { get; set; }
    
}