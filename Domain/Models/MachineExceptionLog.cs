using Domain.Models;

public class MachineExceptionLog : MachineEvent
{
    public int MachineSessionId { get; set; }
    public MachineSession MachineSession { get; set; }
    public MachineExceptionType Type { get; set; }
    public double? TimeSpentSeconds { get; set; }
}