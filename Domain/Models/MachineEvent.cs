using Domain.Models;
using System.ComponentModel.DataAnnotations;

public abstract class MachineEvent
{
    [Key]
    public int MachineEventId { get; set; }
    public int? MachineSessionId { get; set; }
    public MachineSession MachineSession { get; set; }
    public int? MachineId { get; set; }
    public Machine Machine { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
