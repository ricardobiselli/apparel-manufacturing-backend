using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{

    public class CreateGarmentDTO
    {
        [Required]
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }

        // move later?
        public List<OperationDTO>? Operations { get; set; }



    }
}



