namespace Domain.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public int PostNumber { get; set; }
        public string MachineName { get; set; }
        public string MachineModel { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public DateOnly InstallDate { get; set; }
        public bool IsAvailabe { get; set; } = true;
        public Machine() { }
        public Machine(int postNumber, string machineName, string machineModel, DateOnly purchaseDate, DateOnly installDate)
        {
            PostNumber = postNumber;
            MachineName = machineName;
            MachineModel = machineModel;
            PurchaseDate = purchaseDate;
            InstallDate = installDate;

        }

    }
}
