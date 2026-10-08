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
    public class BundleController : ControllerBase
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var list = await _bundleService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("Get-One/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var dto = await _bundleService.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpGet("GetByCutBatch/{cutBatchId}")]
        public async Task<ActionResult> GetByCutBatch([FromRoute] int cutBatchId)
        {
            var list = await _bundleService.GetByCutBatchIdAsync(cutBatchId);
            return Ok(list);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateBundleDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _bundleService.AddAsync(dto);
            return Ok(created);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, CreateBundleDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _bundleService.UpdateAsync(dto, id);
            return NoContent();
        }
    }
}
