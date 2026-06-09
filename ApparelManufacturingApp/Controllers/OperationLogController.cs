using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApparelManufacturingApp.Controllers;

[Route("api/operationlogs")]
[ApiController]
[AllowAnonymous]

public class OperationLogController : ControllerBase
{
    private readonly IOperationLogService _operationLogService;
    private readonly IMachineSessionTimeCalculator _machineSessionTimeCalculator;

    public OperationLogController(IOperationLogService operationLogService, IMachineSessionTimeCalculator machineSessionTimeCalculator)
    {
        _operationLogService = operationLogService;
        _machineSessionTimeCalculator = machineSessionTimeCalculator;
    }

    [HttpGet("GetOperationLogs")]
    public async Task<ActionResult<ICollection<OperationLogDTO>>> GetAllOperationLogs()
    {
        var operationsLog = await _operationLogService.GetAllOperationLogsAsync();
        return Ok(operationsLog);
    }

    [HttpGet("GetExceptionsLogs")]
    public async Task<ActionResult<ICollection<MachineExceptionLogDTO>>> GetAllExceptionLogs()
    {
        var exceptionLogs = await _operationLogService.GetAllExceptionLogsAsync();
        return Ok(exceptionLogs);
    }

    [HttpGet("GetTimeSegmentsForMachineSession/{machineSessionId}")]
    public async Task<ActionResult<ICollection<TimeSegmentDTO>>>
    GetTimeSegmentsForMachineSession(int machineSessionId)
    {
        var timeSegments = await _machineSessionTimeCalculator.Calculate(machineSessionId);
        return Ok(timeSegments);
    }

    [HttpGet("GetOperationLog/{id}")]
    public async Task<ActionResult<OperationLogDTO>> GetOperationLogById([FromRoute] int id)
    {
        var operationLog = await _operationLogService.GetOperationLogByIdAsync(id);
        if (operationLog == null) return NotFound();

        return Ok(operationLog);
    }

    [HttpGet("GetExceptionLogLog/{id}")]
    public async Task<ActionResult<MachineExceptionLogDTO>> GetMachineExceptionLogById([FromRoute] int id)
    {
        var exceptionLog = await _operationLogService.GetMachineExceptionLogByIdAsync(id);
        if (exceptionLog == null) return NotFound();

        return Ok(exceptionLog);
    }

    [HttpPost("CreateOperationLog")]
    public async Task<ActionResult> Create([FromBody] AddOperationLogDTO addOperationLogDto)
    {
        var createdOperationLog = await _operationLogService.AddOperationLogAsync(addOperationLogDto);

        return CreatedAtAction(nameof(GetOperationLogById), new { id = createdOperationLog.MachineSessionId }, createdOperationLog);
    }

    [HttpPost("CreateExceptionLog")]
    public async Task<ActionResult> Create([FromBody] AddMachineExceptionLogDTO machineExceptionLogDTO)
    {
        var createdExceptionLog = await _operationLogService.AddMachineExceptionLogAsync(machineExceptionLogDTO);

        return CreatedAtAction(nameof(GetMachineExceptionLogById), new { id = createdExceptionLog.MachineEventId }, createdExceptionLog);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateOperationLogDTO operationLogDTO)
    {
        await _operationLogService.UpdateAsync(operationLogDTO, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var operationLog = await _operationLogService.GetOperationLogByIdAsync(id);
        await _operationLogService.DeleteAsync(id);
        return NoContent();
    }
}

