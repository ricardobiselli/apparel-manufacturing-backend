using Application.Models.Requests;

namespace Application.Services;

public class ProductionMetricsService
{
    private readonly IMachineSessionTimeCalculator _timeCalculator;

    public ProductionMetricsService(IMachineSessionTimeCalculator timeCalculator)
    {
        _timeCalculator = timeCalculator;
    }

    private static TimeSpan CalculateProductiveTime(List<TimeSegment> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.Productive)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateDowntimeTime(List<TimeSegment> segments)
    {
        return TimeSpan.FromSeconds(
            segments
            .Where(s => s.Type == SegmentType.Downtime)
            .Sum(s => s.Duration.TotalSeconds));
    }

}