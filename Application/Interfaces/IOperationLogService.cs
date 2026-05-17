using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IOperationLogService
    {
        Task<OperationLogDTO> AddOperationLogAsync(AddOperationLogDTO addOperationLogDTO);
        Task<MachineExceptionLogDTO> AddMachineExceptionLogAsync(AddMachineExceptionLogDTO addMachineExceptionLogDTO);
        Task<List<OperationLogDTO>> GetAllOperationLogsAsync();
        Task<List<MachineExceptionLogDTO>> GetAllExceptionLogsAsync();
        Task<OperationLogDTO> GetOperationLogByIdAsync(int id);
        Task<MachineExceptionLogDTO> GetMachineExceptionLogByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateOperationLogDTO updateOperationLogDTO, int id);
    }
}
