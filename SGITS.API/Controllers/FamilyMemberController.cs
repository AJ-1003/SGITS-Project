using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGITS.App.CQRS;
using SGITS.App.CQRS.FamilyMember.Create;
using SGITS.App.CQRS.FamilyMember.Delete;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.CQRS.FamilyMember.Update;

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
    [HttpPost("Create")]
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
    [HttpGet("GetById/{id:guid}")]
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
    [HttpGet("GetAllForHouse/{houseId:guid}")]
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
    [HttpGet("GetAll")]
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
    [HttpPut("Update")]
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
    [HttpPut("AssignToHouse")]
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
    [HttpPut("RemoveFromHouse")]
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
    [HttpPut("Assign")]
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
    [HttpDelete("Delete")]
    public async Task<NoContentResult> DeleteAsync([FromBody] DeleteFamilyMemberCommand deleteFamilyMemberCommand)
    {
        await _mediator.Send(deleteFamilyMemberCommand);

        return NoContent();
    }
}
