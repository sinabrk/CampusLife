namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IPostRepository
{
    Task<Result<Post>> GetPostList(Guid categoryId, Guid locationId, string searchText, string userId = null, PostStatus status = PostStatus.Approved);
    Task<Result<Post>> GetPostById(Guid id);
    Task<Result<Post>> CreateOrUpdatePost(Post post, string userId, CancellationToken cancellationToken);
    Task<Result<int>> DeletePost(Guid id, string userId, CancellationToken cancellationToken);
}