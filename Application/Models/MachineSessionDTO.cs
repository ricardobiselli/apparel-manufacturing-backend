public class MachineSessionDTO
{
    public int OrderId { get; set; }
    public int MachineSessionId { get; set; }
    public int MachineId { get; set; }
    public int GarmentId { get; set; }
    public string GarmentName { get; set; }
    public int OperationId { get; set; }
    public string OperationName { get; set; }
    public string OperationDescription { get; set; }
    public double BaseTime { get; set; }
    public int UnitsPerGarment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public MachineSessionStatus Status { get; set; }
}