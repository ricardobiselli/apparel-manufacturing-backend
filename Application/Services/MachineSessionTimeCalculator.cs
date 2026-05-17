using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;


public class MachineSessionTimeCalculator : IMachineSessionTimeCalculator
{

    private readonly IMachineSessionRepository _machineSessionRepository;

    public MachineSessionTimeCalculator(IMachineSessionRepository machineSessionRepository)
    {
        _machineSessionRepository = machineSessionRepository;
    }
    public async Task<List<TimeSegment>> Calculate(int sessionId)
    {
        var session = await _machineSessionRepository.GetByIdAsyncIncludingLogs(sessionId);

        var events = session.OperationLogs
            .Cast<MachineEvent>()
            .Concat(session.ExceptionLogs)
            .OrderBy(ev => ev.Timestamp)
            .ToList();

        var segments = new List<TimeSegment>();

        if (events.Count < 2)
            return segments;

        for (int i = 0; i < events.Count - 1; i++)
        {
            var current = events[i];
            var next = events[i + 1];

            segments.Add(new TimeSegment
            {
                Start = GetTimestamp(current),
                End = GetTimestamp(next),
                Type = ResolveSegmentType(current)
            });
        }
        Console.WriteLine("segments: ", segments);
        return segments;
    }


    private static DateTime GetTimestamp(MachineEvent log)
    {
        switch (log)
        {
            case OperationLog op:
                return op.Timestamp;
            case MachineExceptionLog ex:
                return ex.Timestamp;
            default:
                throw new InvalidOperationException("Unknown log type");
        }

    }

    private static SegmentType ResolveSegmentType(object previous)
    {
        switch (previous)
        {
            case OperationLog op:
                return SegmentType.Productive;
            case MachineExceptionLog ex:
                return SegmentType.Downtime;
            default: return SegmentType.Unknown;
        }
    }
}


