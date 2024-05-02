namespace BG.CampusLife.Application.MarketPlace.Queries.GetMarketProperties;

public class MarketPropertyDto : IMapFrom<Property>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ControlType { get; set; }
    public List<string> Options { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Property, MarketPropertyDto>()
            .ForMember(cld => cld.Options,
                opt =>
                    opt.MapFrom(c =>
                        c.Options.Split("^", StringSplitOptions.RemoveEmptyEntries)
                            .Select(i => i.Trim()).ToList()))
            .ForMember(cld => cld.ControlType,
                opt=> opt.MapFrom(c => c.ControlType.ToString()));;
    }
}