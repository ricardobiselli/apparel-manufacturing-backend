using Application.Models;
using Application.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICutBatchService
    {
        Task<List<CutBatchDTO>> GetAllAsync();
        Task<CutBatchDTO> GetByIdAsync(int id);
        Task<CutBatchDTO> AddAsync(CreateCutBatchDTO dto);
        Task UpdateAsync(UpdateCutBatchDTO dto, int id);
        Task RegisterActualsAsync(RegisterCutBatchActualsDTO dto, int id);
    }
}
