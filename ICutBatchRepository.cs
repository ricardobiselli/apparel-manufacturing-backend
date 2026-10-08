csharp Domain\IRepositories\ICutBatchRepository.cs
using Domain.Models;

namespace Domain.IRepositories
{
    public interface ICutBatchRepository
    {
        Task<List<CutBatch>> GetAllWithSizesAsync();
        Task<CutBatch?> GetByIdAsync(int id);
        Task<CutBatch?> GetByIdWithSizesAndBundlesAsync(int id);
        Task<List<CutBatch>> GetByOrderIdAsync(int orderId);
        Task<CutBatch> AddAsync(CutBatch entity);
        Task UpdateAsync(CutBatch entity);
        Task DeleteAsync(int id);
    }
}