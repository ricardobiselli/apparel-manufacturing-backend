using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class AddGarmentDTO
    {

        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public List<OperationDTO> Operations { get; set; }

        

    }
}
