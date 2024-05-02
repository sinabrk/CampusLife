namespace BG.CampusLife.Application.MarketPlace.Queries.GetUserMarketItems;

public class GetUserMarketItemsListHandler : BaseHandler<GetUserMarketItemsListHandler>, IRequestHandler<GetUserMarketItemsListQuery, List<MarketItemsListDto>>
{
    public GetUserMarketItemsListHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<List<MarketItemsListDto>> Handle(GetUserMarketItemsListQuery request,
        CancellationToken cancellationToken)
    {
        var user = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!user.Succeeded) throw new NotFoundException(user.Message);

        var result = await Repositories.MarketRepository.GetMarketsList(userId: user.Entity.Id);

        return Mapper.Map<List<MarketItemsListDto>>(result.Entities);
    }
}