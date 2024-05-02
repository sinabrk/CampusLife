using BG.CampusLife.Application.Categories.Commands.UpsertCategory;

namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateCategory(UpsertCategoryCommand category, CancellationToken cancellationToken);
    Task<Category> UpdateCategory(UpsertCategoryCommand category, CancellationToken cancellationToken);
    Task DeleteCategory(Guid id, CancellationToken cancellationToken);
    Task<Result<Category>> GetListOfCategories(int? level, CategoryTypes categoryType, Guid? parentId, bool status);
    Task<Result<Category>> GetCategoryById(Guid id);
    Task<Result<Category>> GetCategoryByCode(string code);
    Task<Result<Category>> GetCategoryBySlug(string slug);
}