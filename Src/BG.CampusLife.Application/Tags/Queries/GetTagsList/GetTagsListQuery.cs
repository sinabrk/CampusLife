namespace BG.CampusLife.Application.Tags.Queries.GetTagsList;

public class GetTagsListQuery : IRequest<List<TagListDto>>
{
    public string SearchText { get; set; }
}