using Application.Models.Requests;
using Domain.Models;

public interface IMachineSessionTimeCalculator
{
   Task <List<TimeSegmentDTO>> Calculate(int sessionId);
}