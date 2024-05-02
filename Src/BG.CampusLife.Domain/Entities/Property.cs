namespace BG.CampusLife.Domain.Entities;

public class Property : BaseEntity
{
    [Required]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
    [Column(TypeName = "nvarchar(50)")]
    public PropertyControlTypes ControlType { get; set; }
    
    [MaxLength(250)]
    public string Name { get; set; }
    // Separated By a Character like ^ in front
    public string Options { get; set; }

    public bool Required { get; set; }
    
}