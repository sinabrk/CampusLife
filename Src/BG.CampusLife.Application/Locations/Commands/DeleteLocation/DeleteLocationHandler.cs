namespace BG.CampusLife.Application.Locations.Commands.DeleteLocation;

public class DeleteLocationHandler : BaseHandler<DeleteLocationHandler>, IRequestHandler<DeleteLocationCommand>
{
    public DeleteLocationHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.LocationRepository.DeleteLocation(request.Id, cancellationToken);
        if (!result.Succeeded) throw new CampusException(result.Message, (int)result.StatusCode);
        return Unit.Value;
    }
}