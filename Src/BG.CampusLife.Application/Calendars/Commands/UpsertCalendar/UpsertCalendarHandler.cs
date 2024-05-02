namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar;

public class UpsertCalendarHandler : BaseHandler<UpsertCalendarHandler>, IRequestHandler<UpsertCalendarCommand, CalendarDto>
{
    public UpsertCalendarHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    { }

    public async Task<CalendarDto> Handle(UpsertCalendarCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = await UpsertCalendarValidator.Validation(request, Repositories, CurrentUserService);
        var dto = MapToUserCalendarDto(request, currentUserId);
        var result = new UserCalendar();

        if (request.Id == Guid.Empty)
            result = await Repositories.UserCalendarRepository.CreateUserCalendar(dto, cancellationToken);
        else
            result = await Repositories.UserCalendarRepository.UpdateUserCalendar(dto, cancellationToken);

        return Mapper.Map<CalendarDto>(result);
    }

    private static UserCalendar MapToUserCalendarDto(UpsertCalendarCommand request, Guid currentUserId)
    {
        return new UserCalendar()
        {
            Id = request.Id,
            Notes = request.Notes,
            Date = request.Date,
            UserId = currentUserId,
            EntityId = request.EntityId,
            EntityType = request.EntityType,
        };
    }
}