namespace Application.Models.Requests;

public class UpdateMachineSessionDTO
{
    public MachineSessionStatus? Status { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public int? UserId { get; set; }
    // update Snapshot
    public string? OperationName { get; set; }
    public string? OperationDescription { get; set; }
    public double? BaseTime { get; set; }
    public int? UnitsPerGarment { get; set; }
}