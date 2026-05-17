using Domain.Enums;

namespace Domain.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public AlertState AlertStatus = AlertState.Inactive;
        public AlertType AlertType = AlertType.None;
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
    }
}
