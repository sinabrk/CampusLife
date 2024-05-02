namespace BG.CampusLife.Application.MarketPlace.Commands.DeleteMarketItem;

public class DeleteMarketItemCommand : IRequest
{
    public Guid Id { get; set; }
}