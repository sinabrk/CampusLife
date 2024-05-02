namespace BG.CampusLife.Application.Posts.Commands.DeletePost;

public class DeletePostHandler : BaseHandler<DeletePostHandler>, IRequestHandler<DeletePostCommand>
{
    public DeletePostHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PostRepository.DeletePost(request.Id, CurrentUserService.UserId, cancellationToken);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}