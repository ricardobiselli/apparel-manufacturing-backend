using Domain.Models;

public class Size
{
    public int SizeId { get; set; }
    public string Name { get; set; }
    public ICollection<CutBatchSize> CutBatchSizes { get; set; }
    public ICollection<BundleSize> BundleSizes { get; set; }
    public ICollection<OrderGarmentSize> OrderGarmentSizes { get; set; } = new List<OrderGarmentSize>();
}