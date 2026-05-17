using Domain.Models;

namespace Domain.IRepositories
{
    public interface IOperationLogRepository
    {
        Task<OperationLog> AddOperationLogAsync(OperationLog operationLog);
        Task<MachineExceptionLog> AddMachineExceptionLogAsync(MachineExceptionLog entity);
        Task<List<OperationLog>> GetByMachineSessionIdAsync(int machineSessionId);
        Task<List<OperationLog>> GetByOperationIdAsync(int operationId);
        Task<List<OperationLog>> GetAllOperationLogsAsync();
        Task<List<MachineExceptionLog>> GetAllExceptionLogsAsync();
        Task<OperationLog?> GetOperationLogByIdAsync(int id);
        Task<MachineExceptionLog?> GetMachineExceptionLogByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<OperationLog?> GetLastByMachineSessionIdAsync(int machineSessionId);
        Task<List<MachineEvent>> GetAllMachineEvents();

    }
}


