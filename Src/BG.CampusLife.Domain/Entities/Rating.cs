namespace BG.CampusLife.Domain.Entities;

public class Rating : BaseEntity
{
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public RatingScale Stars { get; set; }
    
    public string ParticipantId { get; set; }
}
