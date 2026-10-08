namespace Application.Models.Requests
{
    public class AddOrderGarmentDTO
    {
        public int GarmentId { get; set; }
        public int Quantity { get; set; }
        public List<AddOrderGarmentSizeDTO> Sizes { get; set; } = new();
    }

    public class AddOrderGarmentSizeDTO
    {
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
