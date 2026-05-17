using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IGarmentService
    {
        Task<GarmentDTO> AddAsync(CreateGarmentDTO addGarmentDTO);
        Task<List<GarmentDTO>> GetAllAsync();
        Task<GarmentDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateGarmentDTO updateGarmentDTO, int id);
    }
}
