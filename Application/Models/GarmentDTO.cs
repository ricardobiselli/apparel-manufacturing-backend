namespace Application.Models
{
    public class GarmentDTO
    {

        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public double SAM { get; set; }
        public ICollection<OperationDTO> Operations { get; set; }
    }
}

