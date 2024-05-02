namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity;

public class DeleteUniversityHandler : BaseHandler<DeleteUniversityHandler>, IRequestHandler<DeleteUniversityCommand>
{
    public DeleteUniversityHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(DeleteUniversityCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.UniversityRepository.DeleteUniversity(request.Id, cancellationToken);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}