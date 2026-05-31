namespace Application.Models;

public class ProductionMetricsDTO
{
    public TimeSpan ProductiveTime { get; set; }

    public TimeSpan DowntimeTime { get; set; }

    public double DowntimePercent { get; set; }

    public int ExpectedUnits { get; set; }

    public int ActualUnits { get; set; }

    public double EfficiencyPercent { get; set; }
}