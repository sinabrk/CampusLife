namespace BG.CampusLife.Application.Tags.Commands.DeleteTag;

public class DeleteTagHandler : BaseHandler<DeleteTagHandler>, IRequestHandler<DeleteTagCommand>
{
    public DeleteTagHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.TagRepository.DeleteTag(request.Id, cancellationToken);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}