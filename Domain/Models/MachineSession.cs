namespace Domain.Models
{
    public class MachineSession
    {
        public int MachineSessionId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int GarmentId { get; set; }
        public Garment Garment { get; set; }
        //public Operation Operation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public MachineSessionStatus Status { get; set; }
        public ICollection<MachineEvent> Events { get; set; }
        //Operation snapshot:
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public string OperationDescription { get; set; }
        public double BaseTime { get; set; }
        public int UnitsPerGarment { get; set; }

        public MachineSession()
        {
            Events = new List<MachineEvent>();
        }
        public MachineSession(int orderId, int machineId, int garmentId, int operationId, MachineSessionStatus status)
        {
            OrderId = orderId;
            MachineId = machineId;
            GarmentId = garmentId;
            OperationId = operationId;
            CreatedAt = DateTime.UtcNow;
            Status = status;
        }
    }

}
