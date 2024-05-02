namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : ICategoryRepository
{
    public async Task<Result<Category>> GetListOfCategories(int? level, CategoryTypes categoryType, Guid? parentId, bool status)
    {
        var result = new Result<Category>
        {
            StatusCode = ResultStatusCodes.Successful,
            Succeeded = true,
            Entities = await _context.Set<Category>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(
                    c => (!level.HasValue || c.Level == level) &&
                         c.CategoryType == categoryType &&
                         (parentId.HasValue ? c.ParentId == parentId.Value : c.ParentId == null) &&
                         c.Status == status
                )
                .AsNoTracking()
                .ToListAsync()
        };

        result.Total = result.Entities.Count;

        return result;
    }

    public async Task<Result<Category>> GetCategoryById(Guid id)
    {
        var result = new Result<Category>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Category>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Message = MessageHelper.ErrorNotFound("Category", id.ToString());
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Succeeded = false;

        return result;
    }

    public async Task<Result<Category>> GetCategoryByCode(string code)
    {
        var result = new Result<Category>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Category>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(c => c.Code == code)
                .AsNoTracking()
                .FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Message = $"Category not found with {code}";
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Succeeded = false;

        return result;
    }

    public async Task<Result<Category>> GetCategoryBySlug(string slug)
    {
        var result = new Result<Category>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<Category>()
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Where(c => c.Slug == slug)
                .AsNoTracking()
                .FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.Message = $"Category not found with {slug}";
        result.StatusCode = ResultStatusCodes.NotFound;

        return result;
    }

    public async Task<Category> CreateOrUpdateCategory(UpsertCategoryCommand dto, CancellationToken cancellationToken)
    {
        if (dto.Id == Guid.Empty)
            return await DoCreate(dto, cancellationToken);

        return await DoUpdate(dto, cancellationToken);
    }


    public async Task DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Category>()
            .Where(c => c.Id == id)
            .Where(x => x.ParentId != id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            throw new BadRequestException($"You can not delete a main repository. Category '{entity.Title}' has some child categories!");

        entity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Category> DoCreate(UpsertCategoryCommand dto, CancellationToken cancellationToken)
    {
        dto.Id = Guid.NewGuid();
        var category = MapToEntityModel(dto);
        _context.Set<Category>().Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    private async Task<Category> DoUpdate(UpsertCategoryCommand dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Category>().Where(c => c.Id == dto.Id).FirstOrDefaultAsync(cancellationToken);
        entity = MapToEntityModel(dto);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    private static Category MapToEntityModel(UpsertCategoryCommand category)
    {
        return new Category
        {
            Id = category.Id,
            Title = category.Title,
            CategoryType = category.CategoryType,
            Level = category.Level,
            ParentId = category.ParentId,
            Code = category.Code,
            Slug = category.Slug,
            Status = category.Status,
        };
    }
}