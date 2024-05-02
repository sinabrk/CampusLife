namespace BG.CampusLife.Application.Notifications.Commands.SendNotification;

public class SendNotificationHandler : BaseHandler<SendNotificationHandler>, IRequestHandler<SendNotificationCommand>
{
    public SendNotificationHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Email))
        {
            var applicationUser = await Repositories.UserManagerServices.GetUserByEmailAsync(request.Email);
            if (!applicationUser.Succeeded) throw new NotFoundException(applicationUser.Message);

            var user = await Repositories.UserRepository.GetUserById(applicationUser.Entity.Id);
            if (!user.Succeeded) throw new NotFoundException(user.Message);

            var notification = new Notification()
            {
                Body = request.Body,
                UserId = user.Entity.Id,
                SendDate = DateTime.Now,
                Title = request.Title,
                Visited = false
            };

            await Repositories.NotificationRepository.CreateNotification(notification, cancellationToken);
            await Repositories.NotificationServices.SendNotificationToUser(applicationUser.Entity.Email, notification);
        }
        else
        {
            var roles = await Repositories.UserManagerServices.GetRoles();
            foreach (var rolesEntity in roles.Entities)
            {
                var applicationUsers = await Repositories.UserManagerServices.GetUsers(rolesEntity);
                var campusUsers = await Repositories.UserRepository.GetUserList(applicationUsers.Entities, request.LocationId,
                    request.UniversityId);
                var notifications = campusUsers.Entities.Select(user => new Notification()
                    {
                        Body = request.Body,
                        UserId = user.Id,
                        SendDate = DateTime.Now,
                        Title = request.Title,
                        Visited = false
                    })
                    .ToList();

                await Repositories.NotificationRepository.BulkCreateNotifications(notifications, cancellationToken);

                if (request.LocationId == Guid.Empty || request.UniversityId == Guid.Empty)
                    await Repositories.NotificationServices.SendNotificationToAll(new Notification()
                    {
                        Body = request.Body,
                        SendDate = DateTime.Now,
                        Title = request.Title,
                        Visited = false
                    });
                else
                    foreach (var user in campusUsers.Entities)
                        await Repositories.NotificationServices.SendNotificationToUser(user.Email,
                            notifications.First(n => n.UserId == user.Id));
            }
        }

        return Unit.Value;
    }
}