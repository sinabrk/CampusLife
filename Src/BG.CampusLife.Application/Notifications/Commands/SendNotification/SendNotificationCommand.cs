namespace BG.CampusLife.Application.Notifications.Commands.SendNotification;

public class SendNotificationCommand : IRequest
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string Email { get; set; }
    public Guid LocationId { get; set; }
    public Guid UniversityId { get; set; }
}