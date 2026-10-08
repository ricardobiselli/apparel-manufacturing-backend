using Application.Models;
using Application.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBundleService
    {
        Task<List<BundleDTO>> GetAllAsync();
        Task<BundleDTO> GetByIdAsync(int id);
        Task<List<BundleDTO>> GetByCutBatchIdAsync(int cutBatchId);
        Task<BundleDTO> AddAsync(CreateBundleDTO dto);
        Task UpdateAsync(CreateBundleDTO dto, int id);
    }
}
