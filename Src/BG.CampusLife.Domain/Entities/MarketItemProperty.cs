namespace BG.CampusLife.Domain.Entities;

public class MarketItemProperty : BaseEntity
{
    [Required]
    public Guid MarketItemId { get; set; }
    [ForeignKey("MarketItemId")]
    public MarketItem MarketItem { get; set; }
    
    [Required]
    public Guid PropertyId { get; set; }
    [ForeignKey("PropertyId")]
    public Property Property { get; set; }
    
    [MaxLength(255)]
    public string Value { get; set; }
}