namespace Application.Models.Requests
{
    public class AddMachineSessionDTO
    {
        public int OrderId { get; set; }
        public int MachineId { get; set; }
        public int GarmentId { get; set; }
        public int OperationId { get; set; }
        public MachineSessionStatus Status { get; set; } = MachineSessionStatus.Pending;

    }
}
