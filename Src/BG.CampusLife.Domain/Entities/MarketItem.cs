namespace BG.CampusLife.Domain.Entities;

public class MarketItem : BaseEntity
{
    [Required, MaxLength(300)]
    public string Title { get; set; }
    
    [Required, MaxLength(int.MaxValue)]
    public string Description { get; set; }

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Required]
    public Guid LocationId { get; set; }
    [ForeignKey("LocationId")]
    public Location Location { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public MarketItemStatuses Status { get; set; }

    public ICollection<Tag> Tags { get; set; }
    public ICollection<Document> Attachments { get; set; }
    public ICollection<MarketItemProperty> Properties { get; set; }
}