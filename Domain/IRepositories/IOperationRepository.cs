using Domain.Models;

namespace Domain.IRepositories
{
    public interface IOperationRepository
    {
        Task<List<Operation>> GetAllAsync();
        Task<Operation?> GetByIdAsync(int id);
        Task<Operation> AddAsync(Operation entity);
        Task UpdateAsync(Operation entity);
        Task DeleteAsync(int id);
    }
}