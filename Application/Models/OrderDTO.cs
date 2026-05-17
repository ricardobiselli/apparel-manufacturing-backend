namespace Application.Models
{
    public class OrderDTO
    {
        public OrderStatus Status { get; set; }
        public int OrderId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Description { get; set; }
        public ICollection<OrderGarmentDTO> OrderGarments { get; set; }
        public ICollection<MachineSessionDTO> MachineSessions { get; set; }

    }
}