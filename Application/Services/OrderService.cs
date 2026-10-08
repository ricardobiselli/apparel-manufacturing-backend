using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Exceptions;
using Application.Mappers;
using Application.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMachineSessionRepository _machineSessionRepository;

    public OrderService(IOrderRepository orderRepository, IMachineSessionRepository machineSessionRepository)
    {
        _orderRepository = orderRepository;
        _machineSessionRepository = machineSessionRepository;
    }

    public async Task<OrderDTO> AddAsync(AddOrderDTO addOrderDTO)
    {
        // Validate per-garment sizes if provided
        foreach (var og in addOrderDTO.OrderGarments)
        {
            if (og.Sizes != null && og.Sizes.Any())
            {
                if (og.Sizes.Any(s => s.Quantity < 0))
                    throw new ServiceException("Order garment size quantities cannot be negative.");

                var sum = og.Sizes.Sum(s => s.Quantity);
                if (sum != og.Quantity)
                    throw new ServiceException("Garment quantity must equal the sum of its size quantities.");
            }
            else
            {
                if (og.Quantity < 0)
                    throw new ServiceException("Garment quantity cannot be negative.");
            }
        }

        var order = OrderMapper.ToEntity(addOrderDTO);
        var result = await _orderRepository.AddAsync(order);
        return OrderMapper.ToDto(result);
    }

    public async Task<List<OrderDTO>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(OrderMapper.ToDto).ToList();
    }

    public async Task<OrderDTO> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return OrderMapper.ToDto(order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(UpdateOrderDTO updateOrderDto)
    {
        var existingOrder = await _orderRepository
            .GetByIdAsync(updateOrderDto.OrderId);

        if (existingOrder == null)
        {
            throw new Exception("Order not found");
        }

        existingOrder.Status = updateOrderDto.Status;
        existingOrder.Description = updateOrderDto.Description;

        //if (existingOrder.Status == OrderStatus.Active)
        //{
        //    foreach (var session in existingOrder.MachineSessions)
        //    {
        //        session.Status = MachineSessionStatus.InProgress;
        //        await _machineSessionRepository.UpdateAsync(session);
        //    }
        //}
        await _orderRepository.UpdateAsync(existingOrder);
    }
}

