using Application.Models.Requests;

public class ProductionMetricsDTO
{
    public int OperationId { get; set; }
    public string OperationName { get; set; }
    public double BaseTime { get; set; }
    public int UnitsPerGarment { get; set; }
    public TimeSpan ProductiveTime { get; set; }
    public TimeSpan DowntimeTime { get; set; }
    public TimeSpan MachineIssueTime { get; set; }
    public TimeSpan QualityIssueTime { get; set; }
    public TimeSpan BreakTime { get; set; }
    public int ProducedUnits { get; set; }
    public int ExpectedUnits { get; set; }
    public double CompletionPercentage { get; set; }
    public double AverageSecondsPerUnit { get; set; }
    public double EfficiencyPercentage { get; set; }
    public DateTime? SessionCreateTime { get; set; }
    public DateTime? SessionStartTime { get; set; }
    public DateTime? SessionEndTime { get; set; }
    public List<TimeSegmentDTO> Segments { get; set; }
}