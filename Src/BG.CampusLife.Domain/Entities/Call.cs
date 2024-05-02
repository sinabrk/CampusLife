namespace BG.CampusLife.Domain.Entities;

// TODO : Check the realation and correct it
public class Call : BaseEntity
{
    public Guid SenderId { get; set; }
    [ForeignKey("SenderId")]
    public User Sender { get; set; }

    public Guid ReceiverId { get; set; }
    [ForeignKey("ReceiverId")]
    public User Receiver { get; set; }

    public float Duration { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public CallStatus Status { get; set; }

    public int Type { get; set; }

}
