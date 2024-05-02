namespace BG.CampusLife.Application.MarketPlace.Queries.GetMarketTags;

public class MarketTagDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, MarketTagDto>();
    }
}