namespace BG.CampusLife.Application.Notifications.Commands.SendNotification;

public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationValidator()
    {
        RuleFor(c => c.Title).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(c => c.Body).NotEmpty().NotNull();
    }
}