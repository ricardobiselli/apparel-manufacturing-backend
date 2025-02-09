using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OperationDTO
    {

        //public int OperationId { get; set; }
        public string OperationName { get; set; } 
            //= string.Empty;
        public string OperationDescription { get; set; }
        public decimal TimeAllowed { get; set; }
        //public Garment Garment { get; set; }
        //public int GarmentId { get; set; }

        public static OperationDTO Create(Operation operation)
        {
            return new OperationDTO
            {
                OperationName = operation.OperationName,
                OperationDescription = operation.OperationDescription,
                TimeAllowed = operation.TimeAllowed,
            };
        }


    }


}
