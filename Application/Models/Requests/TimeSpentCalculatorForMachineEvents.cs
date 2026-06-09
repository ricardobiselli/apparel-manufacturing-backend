namespace Application.Models.Requests;

public class TimeSegmentDTO
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public TimeSpan Duration => End - Start;
    public SegmentType Type { get; init; }
}
