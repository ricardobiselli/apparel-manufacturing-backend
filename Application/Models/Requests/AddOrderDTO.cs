namespace Application.Models.Requests
{
    public class AddOrderDTO
    {
        public string Description { get; set; }
        public List<AddOrderGarmentDTO> OrderGarments { get; set; } = new List<AddOrderGarmentDTO>();

    }
}
