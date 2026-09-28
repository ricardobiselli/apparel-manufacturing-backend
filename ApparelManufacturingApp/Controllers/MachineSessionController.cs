using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ApparelManufacturingApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MachineSessionController : ControllerBase
{
    private readonly IMachineSessionService _machineSessionService;

    public MachineSessionController(IMachineSessionService machineSessionService)
    {
        _machineSessionService = machineSessionService;
    }

    [HttpGet("All")]
    public async Task<ActionResult<ICollection<MachineSessionDTO>>> GetAll()
    {
        var machineSessions = await _machineSessionService.GetAllAsync();
        return Ok(machineSessions);
    }

    [HttpGet("GetMachineSessionByMachineId/{id}")]
    public async Task<ActionResult<MachineSessionDTO>> GetActiveMachineSessionByMachineId([FromRoute] int id)
    {

        var machineSessionDto = await _machineSessionService.GetActiveMachineSessionByMachineId(id);
        return Ok(machineSessionDto);
    }

    [HttpGet("GetPending/{machineId}")]
    public async Task<ActionResult<ICollection<MachineSessionDTO>>> GetAllSessionsExceptPendingOrInProgressByMachineId([FromRoute] int machineId)
    {
        var machineSessions = await _machineSessionService.GetAllSessionsExceptPendingOrInProgressByMachineId(machineId);
        return Ok(machineSessions);
    }

    [HttpGet("GetActiveMachineSessionByMachineIdWithDetailsIncluded/{id}")]
    public async Task<ActionResult<MachineSessionDTO>> GetActiveMachineSessionByMachineIdWithDetailsIncluded(int id)
    {
        var dto =
            await _machineSessionService
                .GetActiveMachineSessionByMachineIdWithDetailsIncluded(id);

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<MachineSessionDTO>> GetById([FromRoute] int id)
    {

        var machineSessionDto = await _machineSessionService.GetByIdAsync(id);
        return Ok(machineSessionDto);
    }

    [HttpPost("AddMachineSession")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Add([FromBody] AddMachineSessionDTO addMachineSessionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _machineSessionService.AddAsync(addMachineSessionDTO);
        return Ok();
    }


    // Allow Admins and Operators to update sessions.
    // Operators will have their user id assigned to the session automatically.
    [HttpPut("Update/{id}")]
    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operator))]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateMachineSessionDTO updateMachineSessionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // If caller is an operator, set the UserId from the JWT 'sub' claim to prevent impersonation.
        if (User.IsInRole(nameof(UserRole.Operator)))
        {
            var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (int.TryParse(sub, out var currentUserId))
            {
                updateMachineSessionDTO.UserId = currentUserId;
            }
        }

        await _machineSessionService.UpdateAsync(updateMachineSessionDTO, id);
        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult> Delete(int id)
    {
        var machineSession = await _machineSessionService.GetByIdWithDetailsAsync(id);
        if (machineSession == null)
            return NotFound();
        await _machineSessionService.DeleteAsync(id);
        return NoContent();
    }
}
