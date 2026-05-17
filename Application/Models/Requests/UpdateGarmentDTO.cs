namespace Application.Models.Requests
{
    public class UpdateGarmentDTO
    {
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public List<OperationDTO>? Operations { get; set; }
    }
}
