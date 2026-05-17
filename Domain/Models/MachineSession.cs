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

        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public MachineSessionStatus Status { get; set; }

        public ICollection<OperationLog> OperationLogs { get; set; }
        public ICollection<MachineExceptionLog> ExceptionLogs { get; set; }

        public MachineSession()
        {
            OperationLogs = new List<OperationLog>();
            ExceptionLogs = new List<MachineExceptionLog>();
        }
        public MachineSession(int orderId, int machineId, int garmentId, int operationId, MachineSessionStatus status)
        {
            OrderId = orderId;
            MachineId = machineId;
            GarmentId = garmentId;
            OperationId = operationId;
            StartedAt = DateTime.UtcNow;
            Status = status;
        }
    }

}
