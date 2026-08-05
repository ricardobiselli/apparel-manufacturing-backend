namespace Application.Models.Requests;
public class AddMachineDTO
{
    public int PostNumber { get; set; }
    public string MachineName { get; set; }
    public string MachineModel { get; set; }
    public DateOnly InstallDate { get; set; }
}
