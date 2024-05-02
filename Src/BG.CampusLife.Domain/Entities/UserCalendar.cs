namespace BG.CampusLife.Domain.Entities;

/// <summary>
/// In this entity only we save user preferred dates and events
/// not the whole calendar
/// event is optional so user can use event freely
/// </summary>
public class UserCalendar : BaseEntity
{
    [Required]
    public DateTime Date { get; set; }

    [MaxLength(250)]
    public string Notes { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public Guid EntityId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public EntityTypes EntityType { get; set; }
    
    
}