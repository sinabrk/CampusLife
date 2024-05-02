namespace BG.CampusLife.Domain.Entities;

public class Follow : BaseEntity
{
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User UserNavigation { get; set; }


    public Guid FollowId { get; set; }
    [ForeignKey("FollowId")]
    public User FollowNavigation { get; set; }
}
