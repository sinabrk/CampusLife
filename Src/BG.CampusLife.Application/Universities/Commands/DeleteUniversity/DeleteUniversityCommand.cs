namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity;

public class DeleteUniversityCommand : IRequest
{
    public Guid Id { get; set; }
}
