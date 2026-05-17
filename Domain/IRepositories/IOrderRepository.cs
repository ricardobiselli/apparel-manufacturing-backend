using Domain.Models;

namespace Domain.IRepositories
{
    public interface IOrderRepository
    {

        Task<List<Order>> GetAllAsync();


        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order entity);
        Task DeleteAsync(int id);
        Task<Order> UpdateAsync(Order entity);

    }


}
