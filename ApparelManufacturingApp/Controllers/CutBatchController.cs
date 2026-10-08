using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApparelManufacturingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CutBatchController : ControllerBase
    {
        private readonly ICutBatchService _cutBatchService;

        public CutBatchController(ICutBatchService cutBatchService)
        {
            _cutBatchService = cutBatchService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var list = await _cutBatchService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("Get-One/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var dto = await _cutBatchService.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateCutBatchDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _cutBatchService.AddAsync(dto);
            return Ok(created);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateCutBatchDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _cutBatchService.UpdateAsync(dto, id);
            return NoContent();
        }

        [HttpPost("RegisterActuals/{id}")]
        public async Task<ActionResult> RegisterActuals([FromRoute] int id, RegisterCutBatchActualsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _cutBatchService.RegisterActualsAsync(dto, id);
            return NoContent();
        }
    }
}
