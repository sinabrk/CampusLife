namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IMarketRepository
{
    public async Task<Result<MarketItem>> GetMarketsList(Guid categoryId = default,
        Guid locationId = default,
        List<Guid> tags = null, bool hasImage = false, Guid userId = default, List<Guid> properties = null)
    {
        var result = new Result<MarketItem>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = new List<MarketItem>()
        };

        var query = _context.Set<MarketItem>()
            .Include(item => item.Category)
            .Include(item => item.Location)
            .Include(item => item.User)
            .Include(item => item.Tags)
            .Include(item => item.Properties)
            .Include(item => item.Attachments.OrderByDescending(att => att.IsPrimary))
            .Where(item =>
                (item.Status == MarketItemStatuses.Approved) &&
                (categoryId == default || item.CategoryId == categoryId) &&
                (locationId == default || item.LocationId == locationId) &&
                (userId == default || item.UserId == userId) &&
                (!hasImage || item.Attachments.Count > 0) &&
                (tags == null || item.Tags.Any(marketTag => tags.Any(guid => guid == marketTag.Id))) &&
                (properties == null ||
                 item.Properties.Any(marketProperty => properties.Any(guid => marketProperty.PropertyId == guid)))
            )
            .AsSplitQuery()
            .AsNoTracking();


        result.Entities = await query.ToListAsync();
        result.Total = result.Entities.Count;
        return result;
    }

    public async Task<Result<MarketItem>> GetMarketDetail(Guid id)
    {
        var result = new Result<MarketItem>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<MarketItem>()
                .Include(item => item.Category)
                .Include(item => item.Location)
                .Include(item => item.User)
                .Include(item => item.Tags)
                .Include(item => item.Properties).ThenInclude(property => property.Property)
                .Include(item => item.Attachments.OrderByDescending(att => att.IsPrimary))
                .Where(item => item.Id == id)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync()
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Message = MessageHelper.ErrorNotFound("MarketItem", id.ToString());
        return result;
    }

    public async Task<Result<MarketItem>> CreateOrUpdateMarket(MarketItem marketItem, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<MarketItem>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };

        if (marketItem.Id == Guid.Empty)
        {
            marketItem.Id = Guid.NewGuid();
            _context.Set<MarketItem>().Add(marketItem);
        }
        else
        {
            var entity =
                await _context.Set<MarketItem>()
                    .Include(item => item.Category)
                    .Include(item => item.Location)
                    .Include(m => m.User)
                    .Include(item => item.Tags)
                    .Include(item => item.Properties).ThenInclude(property => property.Property)
                    .Include(item => item.Attachments.OrderByDescending(att => att.IsPrimary))
                    .Where(c => c.Id == marketItem.Id && c.User.UserId == userId)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.Title = marketItem.Title;
                entity.Description = marketItem.Description;
                entity.CategoryId = marketItem.CategoryId;
                entity.LocationId = marketItem.LocationId;
                entity.Status = MarketItemStatuses.Edited;
                entity.Tags = marketItem.Tags.Count > 0 ? marketItem.Tags : entity.Tags;
                entity.Attachments = marketItem.Attachments.Count > 0 ? marketItem.Attachments : entity.Attachments;
                entity.Properties = marketItem.Properties.Count > 0 ? marketItem.Properties : entity.Properties;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("MarketItem", marketItem.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }

            result.Entity = entity;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<Result<int>> DeleteMarket(Guid id, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<int>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        var entity = await _context.Set<MarketItem>()
            .Include(u => u.User)
            .Where(c => c.Id == id && c.User.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("MarketItem", id.ToString());
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