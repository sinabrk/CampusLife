namespace BG.CampusLife.Application.Universities.Queries.GetById;

public class GetUniversityByIdQuery : IRequest<UniversityDto>
{
    public Guid Id { get; set; }   
}