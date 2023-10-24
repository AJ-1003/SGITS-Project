using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGITS.App.CQRS;
using SGITS.App.CQRS.House.Create;
using SGITS.App.CQRS.House.Delete;
using SGITS.App.CQRS.House.Get;
using SGITS.App.CQRS.House.Update;

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
    [HttpPost("Create")]
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
    [HttpGet("GetById/{id:guid}")]
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
    [HttpGet("GetHouseholdReport/{ownerId:guid}")]
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
    [HttpGet("GetAll")]
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
    [HttpPut("Update")]
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
    [HttpDelete("Delete")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteAsync([FromBody] DeleteHouseCommand deleteHouseCommand)
    {
        await _mediator.Send(deleteHouseCommand);

        return NoContent();
    }
}
