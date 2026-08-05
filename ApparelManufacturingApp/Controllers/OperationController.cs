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
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ICollection<OperationDTO>>> GetAll()
        {
            var operations = await _operationService.GetAllAsync();
            return Ok(operations);
        }

        [HttpPost("add-operation")]
        public async Task<IActionResult> Add([FromBody] AddOperationDTO addOperationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _operationService.AddAsync(addOperationDTO);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetById([FromRoute] int id)
        {
            var opDto = await _operationService.GetByIdAsync(id);
            return Ok(opDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOperationDTO updateOperationDTO)
        {
            await _operationService.UpdateAsync(id, updateOperationDTO);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _operationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
