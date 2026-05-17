namespace Application.Models.Requests
{
    public class AddMachineExceptionLogDTO
    {
        public int MachineSessionId { get; set; }
        public MachineExceptionType Type { get; set; }
    }

}
