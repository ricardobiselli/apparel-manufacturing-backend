using System.Collections.Generic;

namespace Application.Models.Requests
{
    public class CreateBundleDTO
    {
        public int CutBatchId { get; set; }
        public List<CreateBundleSizeDTO> Sizes { get; set; } = new();
    }

    public class CreateBundleSizeDTO
    {
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
