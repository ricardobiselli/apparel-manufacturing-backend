using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;


namespace ApparelManufacturingApp.Controllers
{
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

        [HttpGet("GetPending")]
        public async Task<ActionResult<ICollection<MachineSessionDTO>>> GetPendingSessionsForActiveOrdersByMachineId([FromQuery] int machineId)
        {
            var machineSessions = await _machineSessionService.GetPendingSessionsForActiveOrdersByMachineId(machineId);
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
        public async Task<IActionResult> Add([FromBody] AddMachineSessionDTO addMachineSessionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _machineSessionService.AddAsync(addMachineSessionDTO);
            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateMachineSessionDTO updateMachineSessionDTO)
        {
            await _machineSessionService.UpdateAsync(updateMachineSessionDTO, id);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var machineSession = await _machineSessionService.GetByIdWithDetailsAsync(id);
            if (machineSession == null)
                return NotFound();
            await _machineSessionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
