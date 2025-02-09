using Application.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class GarmentDTO
    {

        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public ICollection<OperationDTO> Operations { get; set; }


        public static GarmentDTO Create(Garment garment)
        {
            return new GarmentDTO
            {
                GarmentId = garment.GarmentId,
                GarmentName = garment.GarmentName,
                GarmentDescription = garment.GarmentDescription,
                Operations = garment.Operations.Select(OperationDTO.Create).ToList() ?? new List<OperationDTO>()
            };
        }
    }
}

