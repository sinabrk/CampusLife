namespace BG.CampusLife.Presentation.Controllers;

public class CategoryController : BaseController<CategoryController>
{
    /// <summary>
    /// Get List of Categories
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetList([FromQuery]GetCategoriesListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get Category By Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetById([FromQuery]GetCategoryByIdQuery query) => Ok(await Mediator.Send(query));

    /// <summary>
    /// Get Category By Slug
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetBySlug([FromQuery]GetCategoryBySlugQuery query) => Ok(await Mediator.Send(query));

    /// <summary>
    /// Get Category By Code
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetByCode([FromQuery]GetCategoryByCodeQuery query) => Ok(await Mediator.Send(query));

    
    /// <summary>
    /// Insert / Update Category Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryDto>> Upsert([FromBody]UpsertCategoryCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete Category Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeleteCategoryCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

}