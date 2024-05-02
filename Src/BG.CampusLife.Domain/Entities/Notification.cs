namespace BG.CampusLife.Domain.Entities;

public class Notification : BaseEntity
{
    [MaxLength(255), Required]
    public string Title { get; set; }
    [Required, Column(TypeName = "ntext")]
    public string Body { get; set; }
    [Required]
    public bool Visited { get; set; } = false;
    [Required]
    public DateTime SendDate { get; set; }
    public DateTime? SeenDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

}