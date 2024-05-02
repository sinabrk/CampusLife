namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IPropertyRepository
{
    public async Task<Result<Property>> GetPropertyList(string searchText, Guid categoryId)
    {
        var result = new Result<Property>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = await _context.Set<Property>()
                .Include(item => item.Category)
                .Where(item =>
                    (string.IsNullOrEmpty(searchText) || item.Name.Contains(searchText)) &&
                    item.CategoryId == categoryId
                ).ToListAsync()
        };
        result.Total = result.Entities.Count;
        return result;
    }

    public async Task<Result<Property>> GetPropertyById(Guid id)
    {
        var result = new Result<Property>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Property>()
                .Include(item => item.Category)
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync(),
        };
        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Message = $"Property not found with {id}";
        return result;
    }

    public async Task<Result<Property>> CreateOrUpdateProperty(Property property, CancellationToken cancellationToken)
    {
        var result = new Result<Property>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };
        if (property.Id == Guid.Empty)
        {
            property.Id = Guid.NewGuid();
            _context.Set<Property>().Add(property);
        }
        else
        {
            var entity =
                await _context.Set<Property>()
                    .Where(c => c.Id == property.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (entity is not null)
            {
                entity.CategoryId = property.CategoryId;
                entity.Name = property.Name;
                entity.Options = property.Options;
                entity.Required = property.Required;
                entity.ControlType = property.ControlType;
            }
            else
            {
                result.Succeeded = false;
                result.Message = MessageHelper.ErrorNotFound("Property", property.Id.ToString());
                result.StatusCode = ResultStatusCodes.NotFound;
                return result;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        result.Entity = property;

        return result;
    }

    public async Task<Result<Property>> GetUsedMarketProperties()
    {
        var result = new Result<Property>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = await _context.Set<MarketItemProperty>()
                .Include(item => item.Property)
                .Select(item => item.Property)
                .Distinct()
                .ToListAsync(),
        };

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<MarketItemProperty>> CreateMarketItemProperties(Dictionary<string, string> properties)
    {
        var result = new Result<MarketItemProperty>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = new List<MarketItemProperty>()
        };

        foreach (var keyProperty in properties.Keys)
        {
            var propertyEntity = await _context.Set<Property>().FirstOrDefaultAsync(p =>
                p.Id == Guid.Parse(keyProperty.Replace("Property-", "")));

            result.Entities.Add(new MarketItemProperty()
            {
                Property = propertyEntity,
                PropertyId = propertyEntity.Id,
                Value = properties["keyProperty"],
            });
        }


        return result;
    }

    public async Task<Result<int>> DeleteProperty(Guid id, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent
        };
        var entity = await _context.Set<Property>().Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("Property", id.ToString());
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