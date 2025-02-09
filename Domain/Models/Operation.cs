using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

    namespace Domain.Models
    {
        public class Operation
        {
            public int OperationId { get; set; }
            public string OperationName { get; set; } 
            public string OperationDescription { get; set; }
            public decimal TimeAllowed { get; set; }
            public Garment Garment { get; set; }
            public int GarmentId { get; set; }


            public Operation(string operationName, string operationDescription, decimal timeAllowed) {
                OperationName = operationName;  
                OperationDescription = operationDescription;
                TimeAllowed = timeAllowed;
        
            }
        }
    }
