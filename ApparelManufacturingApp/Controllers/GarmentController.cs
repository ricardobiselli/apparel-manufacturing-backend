using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;


namespace ApparelManufacturingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var garments = await _garmentService.GetGarmentsAsync();
         

            var garmentsDTOs = garments.Select(GarmentDTO.Create).ToList();

            return Ok(garmentsDTOs);
        }

        [HttpPost("AddGarment")]
        public async Task<ActionResult<AddGarmentDTO>> AddGarment(AddGarmentDTO addGarmentDTO)
        {
            await _garmentService.AddAsync(addGarmentDTO);
            return Ok();

        }
    }
}
