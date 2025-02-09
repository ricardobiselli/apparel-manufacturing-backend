namespace Domain.Models
{
    public class Garment
    {
        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public ICollection<Operation> Operations { get; set; }

        public Garment () { }

        public Garment(string garmentName, string garmentDescription, ICollection<Operation> operations)
        {
            GarmentName = garmentName;
            GarmentDescription = garmentDescription;
            Operations = operations ?? new List<Operation>();
        }
    }
}
