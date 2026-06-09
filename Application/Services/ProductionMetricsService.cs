using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class ProductionMetricsService : IProductionMetricsService
{
    private readonly IMachineSessionTimeCalculator _timeCalculator;
    private readonly IMachineSessionRepository _machineSessionRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IGarmentRepository _garmentRepository;

    public ProductionMetricsService(IMachineSessionTimeCalculator timeCalculator, IMachineSessionRepository machineSessionRepository, IOrderRepository orderRepository, IGarmentRepository garmentRepository)
    {
        _timeCalculator = timeCalculator;
        _garmentRepository = garmentRepository;
        _orderRepository = orderRepository;
        _machineSessionRepository = machineSessionRepository;
    }

    public async Task<ProductionMetricsDTO> CalculateMetrics(int sessionId)
    {
        var currentSession = await _machineSessionRepository.GetByIdWithDetails(sessionId);

        var currentOrder =
            await _orderRepository.GetByIdAsync(currentSession.OrderId);

        var orderGarment = currentOrder.OrderGarments
            .FirstOrDefault(og => og.GarmentId == currentSession.GarmentId);

        var quantity = orderGarment.Quantity;

        var segments = await _timeCalculator.Calculate(sessionId);

        var productiveTime = CalculateProductiveTime(segments);
        var downtimeTime = CalculateDowntimeTime(segments);
        var machineIssueTime = CalculateMachineIssueTime(segments);
        var qualityIssueTime = CalculateQualityIssueTime(segments);
        var breakTime = CalculateBreakTime(segments);

        var expectedUnits =
            currentSession.Operation.UnitsPerGarment * quantity;

        var producedUnits =
            currentSession.Events
            .Where(e => e is OperationLog).Count();
        
        //if (currentSession.EndedAt != null)
        //{
        //    producedUnits += 1;
        //}

        var completionPercentage =
            expectedUnits == 0
                ? 0
                : (double)producedUnits / expectedUnits * 100;

        var averageSecondsPerUnit =
            producedUnits == 0
                ? 0
                : productiveTime.TotalSeconds / producedUnits;

        var efficiencyPercentage =
            productiveTime.TotalSeconds == 0
                ? 0
                : (producedUnits * currentSession.Operation.BaseTime)
                    / productiveTime.TotalSeconds * 100;

        return new ProductionMetricsDTO
        {
            OperationId = currentSession.Operation.OperationId,
            OperationName = currentSession.Operation.OperationName,
            BaseTime = currentSession.Operation.BaseTime,
            UnitsPerGarment = currentSession.Operation.UnitsPerGarment,
            ProductiveTime = productiveTime,
            DowntimeTime = downtimeTime,
            MachineIssueTime = machineIssueTime,
            QualityIssueTime = qualityIssueTime,
            BreakTime = breakTime,
            ExpectedUnits = expectedUnits,
            ProducedUnits = producedUnits,
            CompletionPercentage = completionPercentage,
            AverageSecondsPerUnit = averageSecondsPerUnit,
            EfficiencyPercentage = efficiencyPercentage,
            SessionEndTime = currentSession.EndedAt,
            Segments = segments
        };
    }
    private static TimeSpan CalculateProductiveTime(List<TimeSegmentDTO> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.Productive)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateDowntimeTime(List<TimeSegmentDTO> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s =>
                    s.Type == SegmentType.QualityIssue ||
                    s.Type == SegmentType.MachineIssue ||
                    s.Type == SegmentType.Break)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateMachineIssueTime(List<TimeSegmentDTO> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.MachineIssue)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateQualityIssueTime(List<TimeSegmentDTO> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.QualityIssue)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateBreakTime(List<TimeSegmentDTO> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.Break)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

}