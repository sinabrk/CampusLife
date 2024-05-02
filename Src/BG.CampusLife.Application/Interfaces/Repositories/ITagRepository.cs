namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface ITagRepository
{
    Task<Result<Tag>> GetTagList(string searchText);
    Task<Result<Tag>> GetTagById(Guid id);
    Task<Result<Tag>> CreateOrUpdateTag(Tag tag, CancellationToken cancellationToken);
    Task<Result<int>> DeleteTag(Guid id, CancellationToken cancellationToken);
    Task<Result<Tag>> GetUsedTags();
    Task<Result<Tag>> BulkCreateTags(List<string> tags, User user, CancellationToken cancellationToken);
}