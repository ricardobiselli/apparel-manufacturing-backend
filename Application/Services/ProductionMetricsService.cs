using Application.Models;
using Application.Models.Requests;

namespace Application.Services;

public class ProductionMetricsService
{
    private readonly IMachineSessionTimeCalculator _timeCalculator;

    public ProductionMetricsService(IMachineSessionTimeCalculator timeCalculator)
    {
        _timeCalculator = timeCalculator;
    }

    public async Task<ProductionMetricsDTO> CalculateMetrics(int sessionId)
    {
        var segments = await _timeCalculator.Calculate(sessionId);

        return new ProductionMetricsDTO
        {
            ProductiveTime = CalculateProductiveTime(segments),
            DowntimeTime = CalculateDowntimeTime(segments),
            MachineIssueTime = CalculateMachineIssueTime(segments),
            QualityIssueTime = CalculateQualityIssueTime(segments),
            BreakTime = CalculateBreakTime(segments)
        };
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
                .Where(s =>
                    s.Type == SegmentType.QualityIssue ||
                    s.Type == SegmentType.MachineIssue ||
                    s.Type == SegmentType.Break)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateMachineIssueTime(List<TimeSegment> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.MachineIssue)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateQualityIssueTime(List<TimeSegment> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.QualityIssue)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

    private static TimeSpan CalculateBreakTime(List<TimeSegment> segments)
    {
        return TimeSpan.FromSeconds(
            segments
                .Where(s => s.Type == SegmentType.Break)
                .Sum(s => s.Duration.TotalSeconds)
        );
    }

}