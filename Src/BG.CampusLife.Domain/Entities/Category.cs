namespace BG.CampusLife.Domain.Entities;

public class Category : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; }
    
    [Column(TypeName = "nvarchar(50)")]
    public CategoryTypes CategoryType { get; set; }
    
    public int Level { get; set; }

    public Guid? ParentId { get; set; }
    public Category Parent { get; set; }
    

    [MaxLength(50)]
    public string Code { get; set; }

    [MaxLength(100)]
    public string Slug { get; set; }

    public bool Status { get; set; }
    
    public ICollection<Category> Children { get; set; }
}