using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Application.Interfaces;

namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar;

public class UpsertCalendarValidator : AbstractValidator<UpsertCalendarCommand>
{
    public UpsertCalendarValidator()
    {
        RuleFor(c => c.Notes).NotNull().NotEmpty().MaximumLength(250);
        RuleFor(c => c.EntityId).NotNull().NotEmpty();
        RuleFor(c => c.EntityType).NotNull().NotEmpty();
    }

    public static async Task<Guid> Validation(UpsertCalendarCommand request, IRepositories repos, ICurrentUserService userService)
    {
        var user = await repos.UserRepository.GetUserById(userService.UserId);
        if (!user.Succeeded) throw new NotFoundException(user.Message);

        if (request.Id != Guid.Empty)
        {
            var userCalendar = await repos.GetEntityById<UserCalendar>(request.Id);
            if (userCalendar != null) throw new NotFoundException(nameof(UserCalendar), request.Id);
        }

        return user.Entity.Id;
    }
}