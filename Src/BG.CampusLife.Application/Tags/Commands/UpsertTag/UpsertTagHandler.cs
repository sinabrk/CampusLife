namespace BG.CampusLife.Application.Tags.Commands.UpsertTag;

public class UpsertTagHandler : BaseHandler<UpsertTagHandler>, IRequestHandler<UpsertTagCommand, UpsertTagDto>
{
    public UpsertTagHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<UpsertTagDto> Handle(UpsertTagCommand request, CancellationToken cancellationToken)
    {
        var user = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!user.Succeeded) throw new NotFoundException(user.Message);

        var result = await Repositories.TagRepository.CreateOrUpdateTag(new Tag()
        {
            Id = request.Id,
            Title = request.Title,
            UserId = user.Entity.Id,
            Created = DateTime.Now,
        }, cancellationToken);
        
        if (!result.Succeeded)
            throw new NotFoundException(result.Message);

        return Mapper.Map<UpsertTagDto>(result.Entity);
    }
}