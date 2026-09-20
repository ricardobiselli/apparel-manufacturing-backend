using Domain.Models;

namespace Domain.IRepositories
{
    public interface IFabricRollRepository
    {
        Task<List<FabricRoll>> GetAllAsync();
        Task<FabricRoll?> GetByIdAsync(int id);
        Task<FabricRoll> AddAsync(FabricRoll entity);
        Task UpdateAsync(FabricRoll entity);
        Task DeleteAsync(int id);
    }
}