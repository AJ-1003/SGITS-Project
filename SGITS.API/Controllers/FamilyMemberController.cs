using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGITS.App.CQRS;
using SGITS.App.CQRS.FamilyMember.Create;
using SGITS.App.CQRS.FamilyMember.Delete;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.CQRS.FamilyMember.Update;
using SGITS.App.CQRS.House.Get;

namespace SGITS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyMemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public FamilyMemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new family member
    /// </summary>
    /// <remarks>
    /// Create a new family member
    /// </remarks>
    /// <param name="createFamilyMemberCommand">Create command with family member data</param>
    /// <returns>Returns the Id of the created family member</returns>
    /// <response code="201">The unique identifier of the newly created family member</response>
    /// <response code="400">Validation failure</response>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BaseCommandResponse>> CreateAsync([FromBody] CreateFamilyMemberCommand createFamilyMemberCommand)
    {
        return await _mediator.Send(createFamilyMemberCommand);
    }

    /// <summary>
    /// Get family member by Id
    /// </summary>
    /// <remarks>
    /// Get family member by Id
    /// </remarks>
    /// <param name="id">Family member Id</param>
    /// <returns>Returns family member</returns>
    /// <response code="200">Family member with all the data</response>
    /// <response code="400">Invalid id</response>
    /// <response code="404">Family member not found</response>
    [HttpGet("GetById/{id:guid}")]
    [ProducesResponseType(typeof(FamilyMemberReadModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<FamilyMemberReadModel>> GetByIdAsync(Guid id)
    {
        return await _mediator.Send(new GetFamilyMemberByIdQuery(id));
    }

    /// <summary>
    /// Get all family members for house
    /// </summary>
    /// <remarks>
    /// Get all family members for house
    /// </remarks>
    /// /// <param name="houseId">House Id</param>
    /// <returns>Returns a list of all family members for house</returns>
    /// <response code="200">A list of all family members for house</response>
    /// <response code="400">Invalid id</response>
    /// <response code="404">House not found</response>
    [HttpGet("GetAllForHouse/{houseId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<FamilyMemberReadModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IReadOnlyList<FamilyMemberReadModel>> GetAllForHouseAsync(Guid houseId)
    {
        return await _mediator.Send(new GetAllFamilyMembersForHouseQuery(houseId));
    }

    /// <summary>
    /// Get all family members
    /// </summary>
    /// <remarks>
    /// Get all family members
    /// </remarks>
    /// <returns>Returns a list of all family members</returns>
    /// <response code="200">A list of all family members</response>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(IReadOnlyList<FamilyMemberReadModel>), (int)HttpStatusCode.OK)]
    public async Task<IReadOnlyList<FamilyMemberReadModel>> GetAllAsync()
    {
        return await _mediator.Send(new GetAllFamilyMembersQuery());
    }

    /// <summary>
    /// Update a family member
    /// </summary>
    /// <remarks>
    /// Update a family member
    /// </remarks>
    /// <param name="updateFamilyMemberCommand">Update command with family member data</param>
    /// <returns>Returns the Id of the updated family member</returns>
    /// <response code="200">The unique identifier of the updated family member</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">Family member not found</response>
    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseCommandResponse>> UpdateAsync([FromBody] UpdateFamilyMemberCommand updateFamilyMemberCommand)
    {
        return await _mediator.Send(updateFamilyMemberCommand);
    }

    /// <summary>
    /// Assign family member to a house
    /// </summary>
    /// <remarks>
    /// Assign family member to a house
    /// </remarks>
    /// <param name="assignFamilyMemberToHouseCommand">Assign family member to house with Id</param>
    /// <returns>Returns the Id of the updated family member</returns>
    /// <response code="200">The unique identifier of the assigned family member</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">Family member not found</response>
    [HttpPut("AssignToHouse")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseCommandResponse>> AssignToHouseAsync([FromBody] AssignFamilyMemberToHouseCommand assignFamilyMemberToHouseCommand)
    {
        return await _mediator.Send(assignFamilyMemberToHouseCommand);
    }

    /// <summary>
    /// Remove family member from a house
    /// </summary>
    /// <remarks>
    /// Remove family member from a house
    /// </remarks>
    /// <param name="removeFamilyMemberFromHouseCommand">Remove family member from house with Id</param>
    /// <returns>Returns the Id of the updated family member</returns>
    /// <response code="200">The unique identifier of the removed family member</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">Family member not found</response>
    [HttpPut("RemoveFromHouse")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseCommandResponse>> RemoveFromHouseAsync([FromBody] RemoveFamilyMemberFromHouseCommand removeFamilyMemberFromHouseCommand)
    {
        return await _mediator.Send(removeFamilyMemberFromHouseCommand);
    }

    /// <summary>
    /// Assign family member as owner or dependant
    /// </summary>
    /// <remarks>
    /// Assign family member as owner or dependant
    /// </remarks>
    /// <param name="assignFamilyMemberAsOwnerOrDependantCommand">Assign family member as owner or dependant with Id</param>
    /// <returns>Returns the Id of the assigned family member</returns>
    /// <response code="200">The unique identifier of the assigned family member</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">Family member not found</response>
    [HttpPut("Assign")]
    [ProducesResponseType(typeof(BaseCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseCommandResponse>> AssignAsync([FromBody] AssignFamilyMemberAsOwnerOrDependantCommand assignFamilyMemberAsOwnerOrDependantCommand)
    {
        return await _mediator.Send(assignFamilyMemberAsOwnerOrDependantCommand);
    }

    /// <summary>
    /// Delete a family member
    /// </summary>
    /// <remarks>
    /// Delete a family member
    /// </remarks>
    /// <param name="deleteFamilyMemberCommand">Command with family member data</param>
    /// <returns>Returns no content</returns>
    /// <response code="204">No content</response>
    /// <response code="400">Validation failure</response>
    /// <response code="404">Family member not found</response>
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<NoContentResult> DeleteAsync([FromBody] DeleteFamilyMemberCommand deleteFamilyMemberCommand)
    {
        await _mediator.Send(deleteFamilyMemberCommand);

        return NoContent();
    }
}
