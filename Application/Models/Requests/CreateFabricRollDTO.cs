using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class CreateFabricRollDTO
    {
        [Required]
        public string FabricRollName { get; set; }
        public string Color { get; set; }
        public string? FabricRollDescription { get; set; }
        public double WeightOrLength { get; set; }
        public double Yield { get; set; }
        public int? BarCode { get; set; }
        public string Supplier { get; set; }
    }
}