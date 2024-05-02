namespace BG.CampusLife.Application.MarketPlace.DTOs;

public class MarketItemsListDto : IMapFrom<MarketItem>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Username { get; set; }

    public string CategoryTitle { get; set; }

    public string LocationTitle { get; set; }

    public string ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MarketItem, MarketItemsListDto>().ForMember(cld => cld.CategoryTitle,
                opt => opt.MapFrom(item => item.Category.Title))
            .ForMember(cld => cld.Username,
                opt => opt.MapFrom(item => MessageHelper.UserPresenter(item.User)))
            .ForMember(cld => cld.LocationTitle,
                opt => opt.MapFrom(item => item.Location.Title))
            .ForMember(cld => cld.ImageUrl,
                opt => opt.MapFrom(item => item.Attachments.FirstOrDefault().FilePath.Insert(0, Configs.ApiUrl)));
    }
}