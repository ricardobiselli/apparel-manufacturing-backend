using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IFabricRollService
    {
        Task<FabricRollDTO> AddAsync(CreateFabricRollDTO createFabricRollDTO);
        Task<List<FabricRollDTO>> GetAllAsync();
        Task<FabricRollDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateFabricRollDTO updateFabricRollDTO, int id);
    }
}