using Domain.Enums;

namespace Application.Models
{
    public class MachineDTO
    {
        public int MachineId { get; set; }
        public int PostNumber { get; set; }
        public string MachineName { get; set; }
        public string MachineModel { get; set; }
        public DateOnly InstallDate { get; set; }
        public MachineStatus Status { get; set; }
    }
}