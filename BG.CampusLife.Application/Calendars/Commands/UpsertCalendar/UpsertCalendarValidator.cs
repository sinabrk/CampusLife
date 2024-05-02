using FluentValidation;

namespace BG.CampusLife.Application.Calendars.Commands.UpsertCalendar
{
    public class UpsertCalendarValidator : AbstractValidator<UpsertCalendarCommand>
    {

        public UpsertCalendarValidator()
        {
            RuleFor(c => c.Notes).NotNull().NotEmpty();
            RuleFor(c => c.EntityId).NotNull().NotEmpty();
            RuleFor(c => c.EntityType).NotNull().NotEmpty();
        }
    }
}