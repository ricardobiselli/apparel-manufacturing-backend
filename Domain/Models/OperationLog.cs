namespace Domain.Models
{
    public class OperationLog : MachineEvent
    {
        public int MachineSessionId { get; set; }
        public MachineSession MachineSession { get; set; }
        public OperationLog() { }

        public OperationLog(int machineSessionId)
        {
            MachineSessionId = machineSessionId;
        }
    }

}

