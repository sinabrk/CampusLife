namespace BG.CampusLife.Domain.Entities;

public class Community : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;
    
    public DateTime ModifiedOn { get; set; } = DateTime.Now;

    public PrivacyType PrivacyModus { get; set; } = PrivacyType.Public;

    // [Column("UserAdminId")]
    // public List<string>? AdminId { get; set; }
    
    public List<User> Members { get; set; }

    [JsonIgnore]
    public List<Post> Posts { get; set; }
}
