using Machine = Domain.Models.Machine;


namespace Domain.IRepositories
{
    public interface IMachineRepository
    {
        Task<List<Machine>> GetAllAsync();
        Task<Machine?> GetByIdAsync(int id);
        Task<Machine> AddAsync(Machine entity);
        Task UpdateAsync(Machine entity);
        Task DeleteAsync(int id);
        Task<Machine?> GetByPostNumberAsync(int postNumber);
    }
}