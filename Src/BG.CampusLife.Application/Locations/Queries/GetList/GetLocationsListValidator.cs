namespace BG.CampusLife.Application.Locations.Queries.GetList;

public class GetLocationsListValidator : AbstractValidator<GetLocationsListQuery>
{
    public GetLocationsListValidator()
    {
        RuleFor(c => c.Status).NotNull().WithMessage("Please define status active/inactive");
    }
}