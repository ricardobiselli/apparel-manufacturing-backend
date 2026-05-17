namespace Application.Models.Requests
{
    public class UpdateMachineDTO
    {

        public string PostNumber { get; set; }
        public string MachineName { get; set; }
        public string MachineModel { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime InstallDate { get; set; }


    }
}
