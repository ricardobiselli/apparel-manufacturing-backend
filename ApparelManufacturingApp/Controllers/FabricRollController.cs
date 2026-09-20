using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApparelManufacturingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FabricRollController : ControllerBase
    {
        private readonly IFabricRollService _fabricRollService;
        public FabricRollController(IFabricRollService fabricRollService)
        {
            _fabricRollService = fabricRollService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ICollection<FabricRollDTO>>> GetAll()
        {
            var rolls = await _fabricRollService.GetAllAsync();
            return Ok(rolls);
        }

        [HttpGet("Get-One/{id}")]
        public async Task<ActionResult<FabricRollDTO>> GetById([FromRoute] int id)
        {
            var dto = await _fabricRollService.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpPost("AddFabricRoll")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult> AddFabricRoll(CreateFabricRollDTO createFabricRollDTO)
        {
            await _fabricRollService.AddAsync(createFabricRollDTO);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _fabricRollService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateFabricRollDTO updateFabricRollDTO)
        {
            await _fabricRollService.UpdateAsync(updateFabricRollDTO, id);
            return NoContent();
        }
    }
}
