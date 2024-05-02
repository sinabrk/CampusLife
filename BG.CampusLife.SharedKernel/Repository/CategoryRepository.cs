using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class CategoryRepository : BaseRepository<CampusContext, CategoryRepository>, ICategoryRepository
    {
        public CategoryRepository(CampusContext context, ILogger<CategoryRepository> logger, IMapper mapper) : base(
            context, logger, mapper)
        {
        }

        public async Task<Result<Category>> GetList(int level, CategoryTypes categoryType, Guid? parentId, bool status)
        {
            var result = new Result<Category>
            {
                StatusCode = ResultStatusCodes.Successful,
                Succeeded = true,
                Entities = await Context.Categories.Where(
                        c => c.Level == level &&
                             c.CategoryType == categoryType &&
                             (!parentId.HasValue || c.ParentId == parentId.Value) &&
                             c.Status == status
                    ).Select(c => new Category()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        CategoryType = c.CategoryType,
                        Level = c.Level,
                        ParentId = c.ParentId,
                        Code = c.Code,
                        Slug = c.Slug,
                    })
                    .ToListAsync()
            };

            result.Total = result.Entities.Count;

            return result;
        }

        public async Task<Result<Category>> GetById(Guid id)
        {
            var result = new Result<Category>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Categories.FirstOrDefaultAsync(c => c.Id == id),
            };
            
            if (result.Entity is not null) return result;
            
            result.Message = $"Category not found with {id}";
            result.StatusCode = ResultStatusCodes.NotFound;
            
            return result;
        }

        public async Task<Result<Category>> GetByCode(string code)
        {
            var result = new Result<Category>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Categories.FirstOrDefaultAsync(c => c.Code == code),
            };

            if (result.Entity is not null) return result;

            result.Message = $"Category not found with {code}";
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;
        }

        public async Task<Result<Category>> GetBySlug(string slug)
        {
            var result = new Result<Category>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Categories.FirstOrDefaultAsync(c => c.Slug == slug),
            };

            if (result.Entity is not null) return result;

            result.Succeeded = false;
            result.Message = $"Category not found with {slug}";
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;
        }

        public async Task<Result<Category>> Upsert(Category category)
        {
            var result = new Result<Category>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };
            if (category.Id != Guid.Empty && await Context.Categories.AnyAsync(c => c.Id == category.Id))
            {
                Context.Entry(category).State = EntityState.Modified;
                Context.Update(category);
                await Context.SaveChangesAsync();
                result.Entity = category;
            }
            else if (category.Id == Guid.Empty)
            {
                category.Id = Guid.NewGuid();
                await Context.AddAsync(category);
                await Context.SaveChangesAsync();
                result.Entity = category;
            }
            else
            {
                result.Succeeded = false;
                result.Message = $"Category not found with {category.Id}";
                result.StatusCode = ResultStatusCodes.NotFound;
            }

            return result;
        }

        public async Task<Result<int>> Delete(Guid id)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent
            };
            var entity = await Context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null)
            {
                result.Succeeded = false;
                result.Message = $"Category not found with {id}";
                result.StatusCode = ResultStatusCodes.NotFound;
            }
            else
            {
                Context.Categories.Remove(entity);
                await Context.SaveChangesAsync();
            }

            return result;
        }
        
        public async Task<bool> IsCategoryExists(Guid id)
        {
            return await Context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}