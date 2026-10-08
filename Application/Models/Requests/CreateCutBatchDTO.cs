using System.Collections.Generic;

namespace Application.Models.Requests
{
    public class CreateCutBatchDTO
    {
        public int OrderId { get; set; }
        public int GarmentId { get; set; }
        public int PlannedQuantity { get; set; }
        public List<CreateCutBatchSizeDTO> Sizes { get; set; } = new();
    }

    public class CreateCutBatchSizeDTO
    {
        public int SizeId { get; set; }
        public int PlannedQuantity { get; set; }
    }
} 