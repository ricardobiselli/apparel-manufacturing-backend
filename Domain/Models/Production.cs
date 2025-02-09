using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Production
    {
        public int ProductionId { get; set; }
        public int Quantity { get; set; }
        public Garment Garment { get; set; }
        public int GarmentId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
