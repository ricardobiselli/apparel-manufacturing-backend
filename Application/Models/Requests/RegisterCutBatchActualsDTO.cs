using System.Collections.Generic;

namespace Application.Models.Requests
{
    public class RegisterCutBatchActualsDTO
    {
        public List<RegisterCutBatchSizeActualDTO> Sizes { get; set; } = new();
    }

    public class RegisterCutBatchSizeActualDTO
    {
        public int SizeId { get; set; }
        public int ActualQuantity { get; set; }
    }
}
