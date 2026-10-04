using Domain.Models;

public class OrderGarmentSize
{
    public int OrderId { get; set; }
    public int GarmentId { get; set; }
    public OrderGarment OrderGarment { get; set; }
    public int SizeId { get; set; }
    public Size Size { get; set; }
    public int Quantity { get; set; }
}