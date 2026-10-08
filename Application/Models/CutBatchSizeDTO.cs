namespace Application.Models
{
    public class CutBatchSizeDTO
    {
        public int CutBatchId { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int PlannedQuantity { get; set; }
        public int? ActualQuantity { get; set; }
    }
}