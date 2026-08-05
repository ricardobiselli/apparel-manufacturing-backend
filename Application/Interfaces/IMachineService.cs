using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IMachineService
    {
        Task<MachineDTO> AddAsync(AddMachineDTO addMachineDTO);
        Task<List<MachineDTO>> GetAllAsync();
        Task<MachineDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, UpdateMachineDTO updateMachineDTO);
    }
}
