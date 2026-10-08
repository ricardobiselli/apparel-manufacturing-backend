using Application.Models;
using Domain.Models;
using System.Linq;

namespace Application.Mappers
{
    public static class CutBatchMapper
    {
        public static CutBatchDTO ToDto(CutBatch entity)
        {
            return new CutBatchDTO
            {
                CutBatchId = entity.CutBatchId,
                OrderId = entity.OrderId,
                GarmentId = entity.GarmentId,
                PlannedQuantity = entity.PlannedQuantity,
                ActualQuantity = entity.ActualQuantity,
                CreatedAt = entity.CreatedAt,
                Sizes = entity.Sizes.Select(s => new CutBatchSizeDTO
                {
                    CutBatchId = s.CutBatchId,
                    SizeId = s.SizeId,
                    PlannedQuantity = s.PlannedQuantity,
                    ActualQuantity = s.ActualQuantity
                }).ToList(),
                Bundles = entity.Bundles?.Select(b => BundleMapper.ToDto(b)).ToList() ?? new System.Collections.Generic.List<BundleDTO>()
            };
        }
    }
}
