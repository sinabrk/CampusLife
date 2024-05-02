namespace BG.CampusLife.Application.Posts.DTOs;

public class PostDto : IMapFrom<Post>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public PostStatus Status { get; set; }
    public string Created { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostDto>()
            .ForMember(cld => cld.Created,
                opt =>
                    opt.MapFrom(c => c.Created.ToString("yyyy/MM/dd HH:mm:ss")))
            .ForMember(cld => cld.CategoryName,
                opt =>
                    opt.MapFrom(c => c.Category.Title))
            .ForMember(cld => cld.LocationName,
                opt =>
                    opt.MapFrom(c => c.Location.Title));
    }
}