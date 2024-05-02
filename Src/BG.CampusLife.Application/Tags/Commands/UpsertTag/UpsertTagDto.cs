namespace BG.CampusLife.Application.Tags.Commands.UpsertTag;

public class UpsertTagDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Created { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, UpsertTagDto>()
            .ForMember(cld => cld.Created, opt => opt.MapFrom(c => c.Created.ToString("yyyy/MM/dd HH:mm:ss")));
    }
}