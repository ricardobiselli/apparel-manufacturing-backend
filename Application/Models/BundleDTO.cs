using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class BundleDTO
    {
        public int BundleId { get; set; }
        public int CutBatchId { get; set; }
        public int Quantity { get; set; }
        public BundleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<BundleSizeDTO> Sizes { get; set; } = new();
    }
}
