namespace BG.CampusLife.Application.Tags.Queries.GetTagsList;

public class TagListDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Created { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagListDto>()
            .ForMember(cld => cld.Created, opt => opt.MapFrom(c => c.Created.ToString("yyyy/MM/dd HH:mm:ss")))
            .ForMember(cld => cld.Username, opt => opt.MapFrom(c => c.User.NormalizedEmail));
    }
}