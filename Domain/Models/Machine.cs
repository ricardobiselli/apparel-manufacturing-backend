using Domain.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string MachineModel { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public DateOnly InstallDate { get; set; }
        public ICollection<ServiceLog> ServicesLog { get; set; }


    }
}
