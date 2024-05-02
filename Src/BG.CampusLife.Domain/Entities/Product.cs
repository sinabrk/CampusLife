namespace BG.CampusLife.Domain.Entities;

public class Product : BaseEntity
{
    [MaxLength(250)]
    public string Name { get; set; }

    public decimal Price { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public ProductTypes Type { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    // public Advertise RelatedPost { get; set; }
}
