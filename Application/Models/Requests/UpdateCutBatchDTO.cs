using System.Collections.Generic;

namespace Application.Models.Requests
{
    public class UpdateCutBatchDTO
    {
        public int PlannedQuantity { get; set; }
        public List<CreateCutBatchSizeDTO> Sizes { get; set; } = new();
    }
}
