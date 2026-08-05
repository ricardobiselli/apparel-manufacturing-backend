using Domain.Enums;
namespace Domain.Models;

public class Machine
{
    public int MachineId { get; set; }
    public int PostNumber { get; set; }
    public string MachineName { get; set; }
    public string MachineModel { get; set; }
    public DateOnly InstallDate { get; set; }
    public MachineStatus Status { get; set; } = MachineStatus.Operational;
    public Machine() { }
    public Machine(int postNumber, string machineName, string machineModel, DateOnly installDate)
    {
        PostNumber = postNumber;
        MachineName = machineName;
        MachineModel = machineModel;
        InstallDate = installDate;
    }
}
