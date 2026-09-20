public class Size
{
    public int SizeId { get; set; }
    public string Name { get; set; }

    public ICollection<CutBatchSize> CutBatchSizes { get; set; }
    public ICollection<BundleSize> BundleSizes { get; set; }
}