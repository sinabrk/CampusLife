namespace BG.CampusLife.Application.Tags.Commands.UpsertTag;

public class UpsertTagCommand : IRequest<UpsertTagDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}