namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : ITagRepository
{
    public async Task<Result<Tag>> GetTagList(string searchText)
    {
        var result = new Result<Tag>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = await _context.Set<Tag>()
                .Include(u => u.User)
                .Where(item => string.IsNullOrEmpty(searchText) || item.Title.Contains(searchText)).ToListAsync()
        };
        result.Total = result.Entities.Count;
        return result;
    }

    public async Task<Result<Tag>> GetTagById(Guid id)
    {
        var result = new Result<Tag>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Tag>().Include(u => u.User).FirstOrDefaultAsync(item => item.Id == id),
        };
        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Message = MessageHelper.ErrorNotFound("Tag", id.ToString());
        return result;
    }

    public async Task<Result<Tag>> CreateOrUpdateTag(Tag tag, CancellationToken cancellationToken)
    {
        var result = new Result<Tag>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };
        if (tag.Id == Guid.Empty)
        {
            tag.Id = Guid.NewGuid();
            _context.Set<Tag>().Add(tag);
        }
        else
        {
            var entity =
                await _context.Set<Tag>()
                    .Where(c => c.Id == tag.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.Title = tag.Title;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("Tag", tag.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        result.Entity = tag;

        return result;
    }

    public async Task<Result<int>> DeleteTag(Guid id, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent
        };
        var entity = await _context.Set<Tag>().Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("Tag", id.ToString());
            result.StatusCode = ResultStatusCodes.NotFound;
        }
        else
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public Task<Result<Tag>> GetUsedTags()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Tag>> BulkCreateTags(List<string> tags, User user, CancellationToken cancellationToken)
    {
        var result = new Result<Tag>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
            Entities = new List<Tag>()
        };
        
        foreach (var tag in from str in tags
            let info = CultureInfo.CurrentCulture.TextInfo
            select info.ToTitleCase(str))
        {
            var tagEntity = await _context.Set<Tag>().Where(item => item.Title == tag).FirstOrDefaultAsync(cancellationToken);
            if (tagEntity is null)
            {
                tagEntity = new Tag()
                {
                    Title = tag,
                    UserId = user.Id
                };
                _context.Set<Tag>().Add(tagEntity);
            }
            result.Entities.Add(tagEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}