namespace BG.CampusLife.Domain.Entities;

public class University : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
    public List<string> Departments { get; set; }
    public bool Status { get; set; }

    public Guid LocationId { get; set; }
    [ForeignKey("LocationId")]
    public Location Location { get; set; }

    public ICollection<User> Users { get; set; }

}
