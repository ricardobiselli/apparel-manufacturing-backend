using Application.Models;
using Application.Models.Requests;
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
                        OperationName = ms.Operation.OperationName,
                        StartedAt = ms.StartedAt,
                        EndedAt = ms.EndedAt,
                        Status = ms.Status
                    })
                    .ToList()
            };
        }
    }
}