namespace Application.Models
{
    public class MachineExceptionLogDTO
    {
        public int MachineEventId { get; set; }
        public int? MachineSessionId { get; set; }
        public MachineExceptionType Type { get; set; }
        public DateTime Timestamp { get; set; }
    }

}

