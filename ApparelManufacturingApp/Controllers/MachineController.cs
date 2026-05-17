using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApparelManufacturingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]

    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ICollection<MachineDTO>>> GetAll()
        {

            var machines = await _machineService.GetAllAsync();



            return Ok(machines);
        }

        [HttpPost("add-machine")]
        public async Task<IActionResult> Add([FromBody] AddMachineDTO addMachineDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _machineService.AddAsync(addMachineDTO);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MachineDTO>> GetById([FromRoute] int id)
        {
            
            var machineDto = await _machineService.GetByIdAsync(id);
            return Ok(machineDto);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
      
            var machine = await _machineService.GetByIdAsync(id);
            await _machineService.DeleteAsync(id);
            return NoContent();
        }

    }
}
