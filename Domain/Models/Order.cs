using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public ICollection<Production> Productions { get; set; }
    }
}
