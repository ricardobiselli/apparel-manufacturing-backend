using Domain.Models;

namespace Domain.IRepositories
{
    public interface IGarmentRepository
    {

        Task<List<Garment>> GetAllGarmentsWithOperationsIncludedAsync();
        Task<List<Garment>> GetAllAsync();
        Task<Garment?> GetByIdAsync(int id);
        Task<Garment?> GetByIdWithOperationsAsync(int id);
        Task<Garment> AddAsync(Garment entity);
        Task UpdateAsync(Garment entity);
        Task DeleteAsync(int id);

    }
}