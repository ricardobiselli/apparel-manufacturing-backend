using Domain.Models;

public class MachineExceptionLog : MachineEvent
{
    public MachineExceptionType Type { get; set; }
}