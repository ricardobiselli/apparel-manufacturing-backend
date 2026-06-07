public class ProductionMetricsDTO
{
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
}