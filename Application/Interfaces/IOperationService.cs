using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IOperationService
    {
        Task<OperationDTO> AddAsync(AddOperationDTO addOperationDTO);
        Task<List<OperationDTO>> GetAllAsync();
        Task<OperationDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, UpdateOperationDTO updateOperationDTO);
    }
}
