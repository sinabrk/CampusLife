namespace BG.CampusLife.Application.MarketPlace.DTOs;

public class MarketItemDto : IMapFrom<MarketItem>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid UserId { get; set; }
    public string Username { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryTitle { get; set; }

    public Guid LocationId { get; set; }

    public string LocationTitle { get; set; }

    public List<string> Pictures { get; set; }

    public Dictionary<string, string> Properties { get; set; }

    public List<string> Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MarketItem, MarketItemDto>().ForMember(cld => cld.CategoryTitle,
                opt => opt.MapFrom(item => item.Category.Title))
            .ForMember(cld => cld.Username,
                opt => opt.MapFrom(item => MessageHelper.UserPresenter(item.User)))
            .ForMember(cld => cld.LocationTitle,
                opt => opt.MapFrom(item => item.Location.Title))
            .ForMember(cld => cld.Pictures,
                opt => opt.MapFrom(item => item.Attachments.Select(att => att.FilePath.Insert(0, Configs.ApiUrl))))
            .ForMember(cld => cld.Tags,
                opt => opt.MapFrom(item => item.Tags.Select(tag => tag.Title)))
            .ForMember(cld => cld.Properties,
                opt => opt.MapFrom(item =>
                    item.Properties.ToDictionary(property => property.Property.Name, property => property.Value)));
    }
}