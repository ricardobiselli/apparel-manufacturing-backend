namespace Domain.Models
{
    public class OrderGarment
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int GarmentId { get; set; }
        public Garment Garment { get; set; }

        public int Quantity { get; set; }
    }
}