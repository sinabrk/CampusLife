using System;
using System.Threading.Tasks;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        // todo add pagination

        /// <summary>
        /// List Category
        /// level and category type is required
        /// if parentId provided, list children
        /// </summary>
        /// <param name="level"></param>
        /// <param name="categoryType"></param>
        /// <param name="parentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<Result<Category>> GetList(int level, CategoryTypes categoryType, Guid? parentId, bool status);
        
        /// <summary>
        /// Fetch category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<Category>> GetById(Guid id);
        
        /// <summary>
        /// Fetch category by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Result<Category>> GetByCode(string code);
        
        /// <summary>
        /// Fetch category by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<Result<Category>> GetBySlug(string slug);

        /// <summary>
        /// Upsert (Insert + Update) Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<Category>> Upsert(Category category);
        
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<int>> Delete(Guid id);
        
        /// <summary>
        /// Check if a parent (category) exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsCategoryExists(Guid id);
    }
}