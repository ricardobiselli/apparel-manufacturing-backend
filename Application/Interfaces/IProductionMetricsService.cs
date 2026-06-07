namespace Application.Interfaces
{
    public interface IProductionMetricsService
    {
        Task<ProductionMetricsDTO> CalculateMetrics(int sessionId);
    }
}
