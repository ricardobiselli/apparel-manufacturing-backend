using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApparelManufacturingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionMetricsController : ControllerBase
    {
        private readonly IProductionMetricsService _productionMetricsService;

        public ProductionMetricsController(IProductionMetricsService productionMetricsService)
        {
            _productionMetricsService = productionMetricsService;
        }

        [HttpGet("{sessionId}")]
        public async Task<ProductionMetricsDTO> Get(int sessionId)
        {
            var metrics = await _productionMetricsService.CalculateMetrics(sessionId);
            return metrics;
        }


    }
}
