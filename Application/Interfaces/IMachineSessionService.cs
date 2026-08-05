using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IMachineSessionService
    {

        Task<MachineSessionDTO> AddAsync(AddMachineSessionDTO addMachineSessionDTO);
        Task<List<MachineSessionDTO>> GetAllAsync();
        Task<MachineSessionDTO> GetByIdAsync(int id);
        Task<MachineSessionDTO> GetByIdWithDetailsAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateMachineSessionDTO updateMachineSessionDTO, int id);
        Task<MachineSessionDTO> GetActiveMachineSessionByMachineId(int id);
        Task<MachineSessionDTO> GetActiveMachineSessionByMachineIdWithDetailsIncluded(int id);
        //Task<ICollection<MachineSessionDTO>> GetPendingSessionsForActiveOrdersByMachineId(int machineId);
        Task<ICollection<MachineSessionDTO>> GetAllSessionsExceptPendingOrInProgressByMachineId(int machineId);


    }
}


