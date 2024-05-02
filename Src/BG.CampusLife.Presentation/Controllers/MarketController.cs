namespace BG.CampusLife.Presentation.Controllers;

public class MarketController : BaseController<MarketController>
{
    /// <summary>
    /// Get List of MarketItems
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MarketItemsListDto>>> GetList([FromQuery]GetMarketItemsListQuery query) => Ok(await Mediator.Send(query));
    
    /// <summary>
    /// Get Market Detail
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MarketItemsListDto>> GetDetail([FromQuery]GetMarketItemDetailQuery query) => Ok(await Mediator.Send(query));

    
    /// <summary>
    /// Insert / Update MarketItem Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<MarketItemsListDto>> Upsert([FromBody]UpsertMarketItemCommand command) => Ok(await Mediator.Send(command));

    /// <summary>
    /// Delete MarketItem Entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete, Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromQuery]DeleteMarketItemCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    /// <summary>
    /// Get List of Used Properties
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MarketPropertyDto>>> GetPopularProperties() => Ok(await Mediator.Send(new GetMarketPropertiesQuery()));
    
    /// <summary>
    /// Get List of Used Tags
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MarketTagDto>>> GetPopularTags() => Ok(await Mediator.Send(new GetMarketTagsQuery()));
    
    /// <summary>
    /// Get List of User MarketItems
    /// </summary>
    /// <returns></returns>
    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MarketItemsListDto>>> GetUserMarketItems() => Ok(await Mediator.Send(new GetUserMarketItemsListQuery()));
}