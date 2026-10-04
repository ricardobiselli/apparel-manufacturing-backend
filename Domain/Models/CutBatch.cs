using Domain.Models;

public class CutBatch
{
    public int CutBatchId { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int GarmentId { get; set; }
    public Garment Garment { get; set; }
    public int PlannedQuantity { get; set; }
    public int? ActualQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<CutBatchSize> Sizes { get; set; }
        = new List<CutBatchSize>();
    public ICollection<Bundle> Bundles { get; set; }
        = new List<Bundle>();
}