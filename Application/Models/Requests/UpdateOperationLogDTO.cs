namespace Application.Models.Requests
{
    public class UpdateOperationLogDTO
    {
        public int OperationLogId { get; set; }
        public int OperationId { get; set; }
        public int? MachineId { get; set; }
        public DateTime Timestamp { get; set; }
        public double TimeSpentSeconds { get; set; }
    }
}





