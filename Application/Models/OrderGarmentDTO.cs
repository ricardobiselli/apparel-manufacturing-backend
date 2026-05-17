namespace Application.Models
{
    public class OrderGarmentDTO
    {
        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public int Quantity { get; set; }
        public List<OperationDTO> Operations { get; set; } = new();
    }
}