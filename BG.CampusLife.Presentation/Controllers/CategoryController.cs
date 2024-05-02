using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Categories.Commands.DeleteCategory;
using BG.CampusLife.Application.Categories.Commands.UpsertCategory;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Application.Categories.Queries.GetByCode;
using BG.CampusLife.Application.Categories.Queries.GetById;
using BG.CampusLife.Application.Categories.Queries.GetBySlug;
using BG.CampusLife.Application.Categories.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Presentation.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        public CategoryController(ILogger<CategoryController> logger) : base(logger)
        {
        }
        
        /// <summary>
        /// Get List Of Categories
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryDto>>> GetList([FromQuery]GetCategoriesListQuery query) => Ok(await Mediator.Send(query));
        
        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryDto>>> GetById([FromQuery]GetCategoryByIdQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// Get Category By Slug
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryDto>>> GetBySlug([FromQuery]GetCategoryBySlugQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// Get Category By Code
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryDto>>> GetByCode([FromQuery]GetCategoryByCodeQuery query) => Ok(await Mediator.Send(query));

        
        /// <summary>
        /// Insert / Update Category Entity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> Upsert([FromBody]UpsertCategoryCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Delete Category Entity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete, Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromQuery]DeleteCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }
}