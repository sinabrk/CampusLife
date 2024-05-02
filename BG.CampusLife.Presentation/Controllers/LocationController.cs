using BG.CampusLife.Application.Locations.Commands.CreateLocation;
using BG.CampusLife.Application.Locations.Commands.DeleteLocation;
using BG.CampusLife.Application.Locations.Commands.DTOs;
using BG.CampusLife.Application.Locations.Commands.UpdateLocation;
using BG.CampusLife.Application.Locations.Queries.DTOs;
using BG.CampusLife.Application.Locations.Queries.GetAll;
using BG.CampusLife.Application.Locations.Queries.GetLocationById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BG.CampusLife.Presentation.Controllers
{
    public class LocationController : BaseController<LocationController>
    {
        public LocationController(ILogger<LocationController> logger) : base(logger) { }

        #region API's

        #region Commands
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateLocationDto>> CreateNewLocation(CreateLocationCommand command) => Ok(await Mediator.Send(command));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateLocationDto>> UpdateLocation(UpdateLocationCommand command) => Ok(await Mediator.Send(command));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLocation([FromQuery] DeleteLocationCommand query) => Ok(await Mediator.Send(query));
        #endregion

        #region Queries
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetLocationDto>> GetLocationById([FromQuery] GetLocationByIdQuery query) => Ok(await Mediator.Send(query));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Domain.Entities.Location>>> GetAllLocations([FromQuery]GetAllLocationQuery query) => Ok(await Mediator.Send(query));
        #endregion

        #endregion
    }
}
