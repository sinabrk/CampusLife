namespace BG.CampusLife.Application.Properties.Commands.DeleteProperty;

public class DeletePropertyHandler : BaseHandler<DeletePropertyHandler>, IRequestHandler<DeletePropertyCommand>
{
    public DeletePropertyHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PropertyRepository.DeleteProperty(request.Id, cancellationToken);
        if (!result.Succeeded)
            throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}