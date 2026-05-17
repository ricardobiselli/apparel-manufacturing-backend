using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IOrderService
    {

        Task<OrderDTO> AddAsync(AddOrderDTO addOrderDTO);
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateOrderDTO updateOrderDto);

    }
}
