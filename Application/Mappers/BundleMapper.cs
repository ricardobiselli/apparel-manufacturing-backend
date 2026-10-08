using Application.Models;
using Domain.Models;
using System.Linq;

namespace Application.Mappers
{
    public static class BundleMapper
    {
        public static BundleDTO ToDto(Bundle entity)
        {
            return new BundleDTO
            {
                BundleId = entity.BundleId,
                CutBatchId = entity.CutBatchId,
                Quantity = entity.Quantity,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                Sizes = entity.Sizes?.Select(s => new BundleSizeDTO
                {
                    BundleId = s.BundleId,
                    SizeId = s.SizeId,
                    Quantity = s.Quantity
                }).ToList() ?? new System.Collections.Generic.List<BundleSizeDTO>()
            };
        }
    }
}
