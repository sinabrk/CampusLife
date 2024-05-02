namespace BG.CampusLife.Domain.Entities;

public class Post : BaseEntity
{
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    
    [MaxLength(500)]
    public string Title { get; set; }

    [MaxLength(int.MaxValue)]
    public string Body { get; set; } 

    public Guid LocationId { get; set; }
    [ForeignKey("LocationId")]
    public Location Location { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PostStatus Status { get; set; }

    public ICollection<Document> Attachments { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
