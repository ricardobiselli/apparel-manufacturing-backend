public class CutBatchSize
{
    public int CutBatchId { get; set; }
    public CutBatch CutBatch { get; set; }
    public int SizeId { get; set; }
    public Size Size { get; set; }
    public int PlannedQuantity { get; set; }
    public int? ActualQuantity { get; set; }
}