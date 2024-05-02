namespace BG.CampusLife.Application.Locations.Commands.DeleteLocation;

public class DeleteLocationCommand : IRequest
{
    public Guid Id { get; set; }
}