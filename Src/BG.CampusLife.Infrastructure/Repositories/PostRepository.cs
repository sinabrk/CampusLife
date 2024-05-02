namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IPostRepository
{
    public async Task<Result<Post>> GetPostList(Guid categoryId, Guid locationId, string searchText, string userId = null, PostStatus status = PostStatus.Approved)
    {
        var result = new Result<Post>
        {
            StatusCode = ResultStatusCodes.Successful,
            Succeeded = true,
            Entities = await _context.Set<Post>()
                .Include(c => c.Category)
                .Include(c => c.User)
                .Include(c => c.Location)
                .Include(c => c.Attachments)
                .Where(p =>
                    (categoryId == Guid.Empty || p.CategoryId == categoryId) &&
                    (locationId == Guid.Empty || p.LocationId == locationId) &&
                    (string.IsNullOrEmpty(searchText) || p.Title.Contains(searchText)) &&
                    (string.IsNullOrEmpty(userId) || p.User.UserId == userId) &&
                    p.Status == status
                )
                .AsNoTracking()
                .ToListAsync()
        };

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<Post>> GetPostById(Guid id)
    {
        var result = new Result<Post>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Post>()
                .Include(c => c.Category)
                .Include(c => c.User)
                .Include(c => c.Location)
                .Include(c => c.Attachments)
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Message = MessageHelper.ErrorNotFound("Post", id.ToString());
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Succeeded = false;

        return result;
    }
    
    public async Task<Result<Post>> CreateOrUpdatePost(Post post, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<Post>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };

        if (post.Id == Guid.Empty)
        {
            post.Id = Guid.NewGuid();
            _context.Set<Post>().Add(post);
        }
        else
        {
            var entity =
                await _context.Set<Post>()
                    .Include(c => c.User)
                    .Include(item => item.Tags)
                    .Include(item => item.Attachments.OrderByDescending(att => att.IsPrimary))
                    .Where(c => c.Id == post.Id && c.User.UserId == userId)
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.Title = post.Title;
                entity.CategoryId = post.CategoryId;
                entity.Body = post.Body;
                entity.LocationId = post.LocationId;
                entity.Status = PostStatus.Edited;
                entity.Tags = post.Tags.Count > 0 ? post.Tags : entity.Tags;
                entity.Attachments = post.Attachments.Count > 0 ? post.Attachments : entity.Attachments;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("Post", post.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }
            
            result.Entity = entity;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
    
    public async Task<Result<int>> DeletePost(Guid id, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent
        };
        var entity = await _context.Set<Post>()
            .Include(c => c.User)
            .Where(c => c.Id == id && c.User.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("Post", $"{id.ToString()}");
            result.StatusCode = ResultStatusCodes.NotFound;
        }
        else
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}