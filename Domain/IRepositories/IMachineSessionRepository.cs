using Domain.Models;

namespace Domain.IRepositories
{
    public interface IMachineSessionRepository
    {
        Task<MachineSession> GetActiveMachineSessionByMachineId(int id);
        Task<List<MachineSession>> GetAllAsync();
        Task<MachineSession?> GetByIdAsync(int id);
        Task<MachineSession> AddAsync(MachineSession entity);
        Task DeleteAsync(int id);
        Task<MachineSession> UpdateAsync(MachineSession entity);
        Task<MachineSession?> GetActiveMachineSessionWithDetailsByMachineId(int machineId);
        Task<MachineSession?> GetByIdAsyncIncludingLogs(int id);
        Task<MachineSession?> GetByIdWithDetails(int id);
        Task<List<MachineSession>> GetPendingSessionsForActiveOrdersByMachineId(int machineId);
        Task<ICollection<MachineSession>> GetAllSessionsExceptPendingOrInProgressByMachineId(int machineId);

    }
}
