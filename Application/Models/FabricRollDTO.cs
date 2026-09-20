using System;

namespace Application.Models
{
    public class FabricRollDTO
    {
        public int FabricRollId { get; set; }
        public string FabricRollName { get; set; }
        public string Color { get; set; }
        public string? FabricRollDescription { get; set; }
        public double WeightOrLength { get; set; }
        public double Yield { get; set; }
        public DateOnly Date { get; set; }
        public int? BarCode { get; set; }
    }
}
