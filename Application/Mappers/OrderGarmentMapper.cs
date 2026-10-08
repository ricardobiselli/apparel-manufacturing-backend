using Application.Models;
using Domain.Models;

namespace Application.Mappers
{
    public static class OrderGarmentMapper
    {
        public static OrderGarmentDTO ToDto(OrderGarment orderGarment)
        {
            return new OrderGarmentDTO
            {
                GarmentId = orderGarment.GarmentId,
                GarmentName = orderGarment.Garment?.GarmentName ?? "Unknown",
                Quantity = orderGarment.Quantity,

                Operations = orderGarment.Garment?.Operations?
                    .Select(OperationMapper.ToDto)
                    .ToList() ?? new List<OperationDTO>(),

                Sizes = orderGarment.Sizes?.Select(s => new OrderGarmentSizeDTO
                {
                    SizeId = s.SizeId,
                    SizeName = s.Size?.SizeName,
                    Quantity = s.Quantity
                }).ToList() ?? new List<OrderGarmentSizeDTO>()
            };
        }
    }
}