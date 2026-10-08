using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Domain.Models;

namespace Application.Mappers
{
    public class OrderMapper
    {
        public static Order ToEntity(AddOrderDTO orderDTO)
        {
            var orderGarments = orderDTO.OrderGarments
                .Select(og => new OrderGarment
                {
                    GarmentId = og.GarmentId,
                    Quantity = og.Quantity,
                    Sizes = og.Sizes?.Select(s => new OrderGarmentSize
                    {
                        SizeId = s.SizeId,
                        Quantity = s.Quantity
                    }).ToList() ?? new List<OrderGarmentSize>()
                })
                .ToList();

            var newOrder = new Order
            {
                Description = orderDTO.Description,
                OrderGarments = orderGarments,
                //DateOfCreation = DateTime.UtcNow
            };
            return newOrder;
        }


        public static OrderDTO ToDto(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                DateOfCreation = order.DateOfCreation,
                Description = order.Description,
                Status = order.Status,

                OrderGarments = order.OrderGarments
                    .Select(OrderGarmentMapper.ToDto)
                    .ToList(),

                MachineSessions = order.MachineSessions
                    .Select(ms => new MachineSessionDTO
                    {
                        MachineSessionId = ms.MachineSessionId,
                        MachineId = ms.MachineId,
                        GarmentId = ms.GarmentId,
                        GarmentName = ms.Garment.GarmentName,
                        OperationId = ms.OperationId,
                        OperationName = ms.OperationName,
                        StartedAt = ms.StartedAt,
                        EndedAt = ms.EndedAt,
                        Status = ms.Status
                    })
                    .ToList(),
                FabricRolls = order.FabricRolls
                 .Select(fr => new FabricRollDTO
                 {
                     FabricRollId = fr.FabricRollId,
                     FabricRollName = fr.FabricRollName,
                     Color = fr.Color,
                     FabricRollDescription = fr.FabricRollDescription,
                     WeightOrLength = fr.WeightOrLength,
                     Yield = fr.Yield,
                     Date = fr.Date,
                     BarCode = fr.BarCode,
                     Supplier = fr.Supplier,
                     State = fr.State

                 }).ToList(),

                CutBatches = order.CutBatches
                    .Select(cb => new CutBatchDTO
                    {
                        CutBatchId = cb.CutBatchId,
                        OrderId = cb.OrderId,
                          GarmentId = cb.GarmentId,
                        PlannedQuantity= cb.PlannedQuantity,
                        ActualQuantity = cb.ActualQuantity,
                        CreatedAt = cb.CreatedAt,
                        Sizes = cb.Sizes
                            .Select(sz=> new CutBatchSizeDTO
                            {
                                    CutBatchId = sz.CutBatchId,
                                SizeId = sz.SizeId,
                                SizeName = sz.Size.SizeName,
                                PlannedQuantity= sz.PlannedQuantity,
                                ActualQuantity = sz.ActualQuantity
                            })
                            .ToList(),
                        Bundles = cb.Bundles
                            .Select(bd=>new BundleDTO
                            {
                                BundleId = bd.BundleId,
                                CutBatchId = bd.CutBatchId,
                                Quantity = bd.Quantity,
                                Status = bd.Status, 
                                CreatedAt = bd.CreatedAt,
                                Sizes = bd.Sizes
                                    .Select(sz => new BundleSizeDTO
                                    {
                                        BundleId = sz.BundleId,
                                        SizeId = sz.SizeId,
                                        Quantity = sz.Quantity
                                    })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList()
            };
        }
    }
}