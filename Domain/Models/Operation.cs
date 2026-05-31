namespace Domain.Models
{
    public class Operation
    {
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public string OperationDescription { get; set; }
        public Garment Garment { get; set; }
        public int GarmentId { get; set; }

        public double BaseTime { get; set; }
        public int UnitsPerGarment { get; set; }

        public Operation() { }

        public Operation(string operationName, string operationDescription, double baseTime, int unitsPerGarment)
        {
            OperationName = operationName;
            OperationDescription = operationDescription;
            BaseTime = baseTime;
            UnitsPerGarment = unitsPerGarment;
        }
    }
}
