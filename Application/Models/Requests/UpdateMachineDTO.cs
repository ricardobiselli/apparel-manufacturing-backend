using Domain.Enums;
namespace Application.Models.Requests;

public class UpdateMachineDTO
{
    public int PostNumber { get; set; }
    public string MachineName { get; set; }
    public string MachineModel { get; set; }
    public DateOnly InstallDate { get; set; }
    public MachineStatus Status { get; set; }
}
