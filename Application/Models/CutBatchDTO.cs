using System;
using System.Collections.Generic;

namespace Application.Models;

public class CutBatchDTO
{
    public int CutBatchId { get; set; }
    public int OrderId { get; set; }
    public int GarmentId { get; set; }
    public int PlannedQuantity { get; set; }
    public int? ActualQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CutBatchSizeDTO> Sizes { get; set; } = new();
    public List<BundleDTO> Bundles { get; set; } = new();
}