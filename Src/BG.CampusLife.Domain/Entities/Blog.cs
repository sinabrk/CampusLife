namespace BG.CampusLife.Domain.Entities;

public class Blog : BaseEntity
{
    [Required, MaxLength(int.MaxValue)]
    public string Description { get; set; }

    [MaxLength(150)]
    public string Summary { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId)")]
    public User User { get; set; }

    public List<Post> Posts { get; set; }
}
