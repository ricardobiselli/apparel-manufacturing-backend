namespace Domain.Models
{
    public class Garment
    {
        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public string GarmentDescription { get; set; }
        public double SAM => Operations?.Sum(o => o.BaseTime / 60) ?? 0;
        public ICollection<Operation> Operations { get; set; }
        public ICollection<OrderGarment> OrderGarments { get; set; }


        public Garment() { }

        public Garment(string garmentName, string garmentDescription, ICollection<Operation> operations)
        {
            GarmentName = garmentName;
            GarmentDescription = garmentDescription;
            Operations = operations ?? new List<Operation>();
        }
    }
}
