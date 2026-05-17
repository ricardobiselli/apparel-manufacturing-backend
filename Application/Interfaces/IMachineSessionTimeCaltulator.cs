using Application.Models.Requests;
using Domain.Models;

public interface IMachineSessionTimeCalculator
{
   Task <List<TimeSegment>> Calculate(int sessionId);
}