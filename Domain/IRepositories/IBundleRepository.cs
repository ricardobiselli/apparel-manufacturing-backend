using Domain.Models;

namespace Domain.IRepositories
{
    public interface IBundleRepository
    {
        Task<List<Bundle>> GetAllAsync();
        Task<Bundle?> GetByIdWithSizesAsync(int id);
        Task<List<Bundle>> GetByCutBatchIdAsync(int cutBatchId);
        Task<Bundle> AddAsync(Bundle entity);
        Task UpdateAsync(Bundle entity);
        Task DeleteAsync(int id);
    }
}