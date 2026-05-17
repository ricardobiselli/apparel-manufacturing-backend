namespace Domain.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime DateOfCreation { get; set; }
    public string Description { get; set; }
    public OrderStatus Status { get; set; } 
    public ICollection<OrderGarment> OrderGarments { get; set; } = new List<OrderGarment>();
    public ICollection<MachineSession> MachineSessions { get; set; } = new List<MachineSession>();

    public Order()
    {
        DateOfCreation = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

}