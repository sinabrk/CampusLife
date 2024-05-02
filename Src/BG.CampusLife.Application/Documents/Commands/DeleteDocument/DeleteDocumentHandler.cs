namespace BG.CampusLife.Application.Documents.Commands.DeleteDocument;

public class DeleteDocumentHandler : BaseHandler<DeleteDocumentHandler>, IRequestHandler<DeleteDocumentCommand>
{
    public DeleteDocumentHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.DocumentRepository.DeleteTempDocument(request.Id, CurrentUserService.UserId, cancellationToken);
        if (!result.Succeeded) throw new NotFoundException(result.Message);            
        return Unit.Value;
    }
}