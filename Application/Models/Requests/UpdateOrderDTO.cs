namespace Application.Models.Requests
{
    public class UpdateOrderDTO
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }

        //public ICollection<OrderGarmentDTO> OrderGarments { get; set; }
        //public ICollection<MachineSessionDTO> MachineSessions { get; set; }
    }
}