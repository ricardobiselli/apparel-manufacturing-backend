namespace Application.Models
{
    public class AddOperationDTO
    {

        public string OperationName { get; set; }
        public string OperationDescription { get; set; }
        public double BaseTime { get; set; }
        public int UnitsPerGarment { get; set; }
    }
}
