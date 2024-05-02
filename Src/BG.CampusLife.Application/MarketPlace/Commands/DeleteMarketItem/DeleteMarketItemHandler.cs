namespace BG.CampusLife.Application.MarketPlace.Commands.DeleteMarketItem;

public class DeleteMarketItemHandler : BaseHandler<DeleteMarketItemHandler>, IRequestHandler<DeleteMarketItemCommand>
{
    public DeleteMarketItemHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}
    
    public async Task<Unit> Handle(DeleteMarketItemCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.MarketRepository.DeleteMarket(request.Id, CurrentUserService.UserId, cancellationToken);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Unit.Value;
    }
}