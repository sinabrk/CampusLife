namespace BG.CampusLife.Application.Universities.Commands.UpsertUniversity;

public class UpsertUniversityCommand : IRequest<PostDto>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid LocationId { get; set; }

    public bool Status { get; set; }

}
