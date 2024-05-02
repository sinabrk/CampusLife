using BG.CampusLife.Application.Universities.Commands.CreateUniversity;
using BG.CampusLife.Application.Universities.Commands.DeleteUniversity;
using BG.CampusLife.Application.Universities.Commands.DTO;
using BG.CampusLife.Application.Universities.Commands.UpdateUniversity;
using BG.CampusLife.Application.Universities.Queries.DTO;
using BG.CampusLife.Application.Universities.Queries.GetAllUniversities;
using BG.CampusLife.Application.Universities.Queries.GetUniversityById;
using BG.CampusLife.Application.Universities.Queries.GetUniversityByName;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BG.CampusLife.Presentation.Controllers
{
    public class UniversityController : BaseController<UniversityController>
    {
        #region Ctor
        public UniversityController(ILogger<UniversityController> logger) : base(logger) { }
        #endregion

        #region API's

        #region Commands
        /// <summary>
        /// Create or update univeristy
        /// </summary>
        /// <param name="university"></param>
        /// <returns>UniversityDTO</returns>
        /// <response code="201">New user has been created</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateOrUpdateUniversityDto>> CreateUniversity(CreateUniversityCommand university) => Ok(await Mediator.Send(university));

        /// <summary>
        /// Delete university by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUniversityById([FromQuery] DeleteUniversityByIdCommand query) => Ok(await Mediator.Send(query));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateOrUpdateUniversityDto>> UpdateUniversity(UpdateUniversityCommand query) => Ok(await Mediator.Send(query));
        #endregion

        #region Quries
        /// <summary>
        /// Get university by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns>UniversityDTO</returns>
        /// <response code="200">New user has been created</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UniQueriesDto>> GetUniversityById([FromQuery] GetUniversityByIdQuery query) => Ok(await Mediator.Send(query));

        /// <summary>
        /// Get all the universities
        /// </summary>
        /// <param name="query"></param>
        /// <returns>UniversityDTO</returns>
        /// <response code="200">New user has been created</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<UniQueriesDto>> GetAllUniversities([FromQuery] GetAllUniversitiesQuery query) => await Mediator.Send(query);

        /// <summary>
        /// Get University by given name
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UniQueriesDto>> GetUniversityByName([FromQuery] GetUniversityByNameQuery query) => Ok(await Mediator.Send(query));
        #endregion

        #endregion
    }
}
