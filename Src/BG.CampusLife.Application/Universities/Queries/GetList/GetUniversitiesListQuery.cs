namespace BG.CampusLife.Application.Universities.Queries.GetList;

public class GetUniversitiesListQuery : IRequest<List<UniversityDto>>
{
    public Guid LocationId { get; set; }
    public string SearchText { get; set; }
}