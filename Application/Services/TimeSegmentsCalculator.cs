using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;


public class TimeSegmentsCalculator : IMachineSessionTimeCalculator
{

    private readonly IMachineSessionRepository _machineSessionRepository;

    public TimeSegmentsCalculator(IMachineSessionRepository machineSessionRepository)
    {
        _machineSessionRepository = machineSessionRepository;
    }
    public async Task<List<TimeSegmentDTO>> Calculate(int sessionId)
    {
        var session = await _machineSessionRepository.GetByIdAsyncIncludingLogs(sessionId);

        var events = session.Events
            .OrderBy(ev => ev.Timestamp)
            .ToList();

        var segments = new List<TimeSegmentDTO>();

        if (events.Count < 2)
            return segments;

        for (int i = 0; i < events.Count - 1; i++)
        {
            var current = events[i];
            var next = events[i + 1];

            segments.Add(new TimeSegmentDTO
            {
                Start = current.Timestamp,
                End = next.Timestamp,
                Type = ResolveSegmentType(current)
            });
        }

        var lastEvent = events.Last();

        segments.Add(new TimeSegmentDTO
        {
            Start = lastEvent.Timestamp,
            End = lastEvent.Timestamp,
            Type = ResolveSegmentType(lastEvent)
        });
        Console.WriteLine("segments: ", segments);

        return segments;
    }

    private static SegmentType ResolveSegmentType(MachineEvent current)
    {
        switch (current)
        {
            case OperationLog op:
                return SegmentType.Productive;
            case MachineExceptionLog ex:
                switch (ex.Type)
                {
                    case MachineExceptionType.FaultyPiece:
                        return SegmentType.QualityIssue;
                    case MachineExceptionType.ThreadBreak:
                    case MachineExceptionType.NeedleBreak:
                        return SegmentType.MachineIssue;
                    case MachineExceptionType.Break:
                        return SegmentType.Break;
                    case MachineExceptionType.EndOfDay:
                        return SegmentType.EndOfDay;
                    case MachineExceptionType.EndOfProduction:
                        return SegmentType.EndOfProduction;
                    default:
                        return SegmentType.Unknown;
                }
            default:
                return SegmentType.Unknown;
        }
    }
}


