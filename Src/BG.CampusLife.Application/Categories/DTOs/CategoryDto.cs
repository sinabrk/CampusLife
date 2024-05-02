namespace BG.CampusLife.Application.Categories.DTOs;

public class CategoryDto : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string CategoryType { get; set; }
    public int Level { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentTitle { get; set; }
    public string Code { get; set; }
    public string Slug { get; set; }
    public bool Status { get; set; }
    public List<CategoryDto> Children { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDto>()
        .ForMember(dto => dto.ParentTitle,
            opt => opt.MapFrom(
                    ent => ent.Parent.Title)
            )
        .ForMember(dto => dto.CategoryType,
            opt => opt.MapFrom(
                ent => ent.CategoryType.ToString()));
    }
}