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

    public class GarmentController : ControllerBase
    {

        private readonly IGarmentService _garmentService;

        public GarmentController(IGarmentService garmentService)
        {
            _garmentService = garmentService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ICollection<GarmentDTO>>> GetGarments()
        {
            var garments = await _garmentService.GetAllAsync();
            return Ok(garments);
        }

        [HttpPost("AddGarment")]
        public async Task<ActionResult> AddGarment(CreateGarmentDTO addGarmentDTO)
        {

            await _garmentService.AddAsync(addGarmentDTO);
            return Ok();

        }

        [HttpGet("Get-One/{id}")]
        public async Task<ActionResult<GarmentDTO>> GetById([FromRoute] int id)
        {

            var garmentDto = await _garmentService.GetByIdAsync(id);
            return Ok(garmentDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            var garment = await _garmentService.GetByIdAsync(id);
            await _garmentService.DeleteAsync(id);
            return NoContent();
        }

        //[HttpPut("Update")]
        //public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreateGarmentForClientsDTO garmentDto)
        //{


        //    await _garmentService.UpdateAsync(garmentDto, id);
        //    return NoContent();
        //}

    }

}
