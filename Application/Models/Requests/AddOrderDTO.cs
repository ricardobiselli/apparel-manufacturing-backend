namespace Application.Models.Requests
{
    public class AddOrderDTO
    {
        public string Description { get; set; }

        //move later?
        public List<AddOrderGarmentDTO> OrderGarments { get; set; } = new List<AddOrderGarmentDTO>();

    }
}
