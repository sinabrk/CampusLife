namespace BG.CampusLife.Domain.Entities;

public class Tag : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; }
    
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public ICollection<MarketItem> MarketItems { get; set; }
    public ICollection<Post> Posts { get; set; }
}