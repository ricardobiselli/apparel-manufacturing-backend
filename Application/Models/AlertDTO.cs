using Domain.Enums;
using Domain.Models;

namespace Application.Models
{
    public class AlertDTO
    {

        public int AlertId { get; set; }
        public AlertState AlertStatus = AlertState.Inactive;
        public AlertType AlertType = AlertType.None;
        public int MachineId { get; set; }

        public static AlertDTO Create(Alert alert)
        {
            return new AlertDTO
            {
                AlertId = alert.AlertId,
                AlertStatus = alert.AlertStatus,
                AlertType = alert.AlertType,
                MachineId = alert.MachineId,
            };
        }
    }

}
