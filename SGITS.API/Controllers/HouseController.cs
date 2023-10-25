using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGITS.App.CQRS;
using SGITS.App.CQRS.House.Create;
using SGITS.App.CQRS.House.Delete;
using SGITS.App.CQRS.House.Get;
using SGITS.App.CQRS.House.Update;
using SGITS.App.Exceptions;

namespace SGITS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public HouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new house
    /// </summary>
    /// <remarks>
    /// Create a new house
    /// </remarks>
    /// <param name="createHouseCommand">Create command with house data</param>
    /// <returns>Returns the Id of the created house</returns>
    /// <response code="201">The unique identifier of the newly created house</response>
    /// <response code="400">Validation failure</response>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BaseCommandResponse>> CreateAsync([FromBody] CreateHouseCommand createHouseCommand)
    {
        return await _mediator.Send(createHouseCommand);
    }

    /// <summary>
    /// Get house by Id
    /// </summary>
    /// <remarks>
    /// Get house by Id
    /// </remarks>
    /// <param name="id">House Id</param>
    /// <returns>Returns the Id of the created house</returns>
    /// <response code="200">House with all the data</response>
    /// <response code="400">Invalid id</response>
    /// <response code="404">House not found</response>
    [HttpGet("GetById/{id:guid}")]
    [ProducesResponseType(typeof(HouseReadModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<HouseReadModel> GetByIdAsync(Guid id)
    {
        return await _mediator.Send(new GetHouseByIdQuery(id));
    }

    /// <summary>
    /// Get household report
    /// </summary>
    /// <remarks>
    /// Get household report
    /// </remarks>
    /// <param name="ownerId">Owner Id</param>
    /// <returns>Returns the household report</returns>
    /// <response code="200">Household Report with all the data</response>
    /// <response code="400">Invalid id</response>
    [HttpGet("GetHouseholdReport/{ownerId:guid}")]
    [ProducesResponseType(typeof(HouseholdReportReadModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<HouseholdReportReadModel> GetReportAsync(Guid ownerId)
    {
        return await _mediator.Send(new GetHouseholdReportQuery(ownerId));
    }

    /// <summary>
    /// Get all houses
    /// </summary>
    /// <remarks>
    /// Get all houses
    /// </remarks>
    /// <returns>Returns a list of all houses</returns>
    /// <response code="200">A list of all houses</response>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(IReadOnlyList<HouseReadModel>), (int)HttpStatusCode.OK)]
    public async Task<IReadOnlyList<HouseReadModel>> GetAllAsync()
    {
        return await _mediator.Send(new GetAllHousesQuery());
    }

    /// <summary>
    /// Update a house
    /// </summary>
    /// <remarks>
    /// Update a house
    /// </remarks>
    /// <param name="updateHouseCommand">Update command with house data</param>
    /// <returns>Returns the Id of the updated house</returns>
    /// <response code="200">The unique identifier of the updated house</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">House not found</response>
    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseCommandResponse>> UpdateAsync([FromBody] UpdateHouseCommand updateHouseCommand)
    {
        return await _mediator.Send(updateHouseCommand);
    }

    /// <summary>
    /// Delete house
    /// </summary>
    /// <remarks>
    /// Delete house
    /// </remarks>
    /// <param name="deleteHouseCommand">Delete command with house data</param>
    /// <returns>Returns no content</returns>
    /// <response code="204">No content</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">House not found</response>
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<NoContentResult> DeleteAsync([FromBody] DeleteHouseCommand deleteHouseCommand)
    {
        await _mediator.Send(deleteHouseCommand);

        return NoContent();
    }
}
