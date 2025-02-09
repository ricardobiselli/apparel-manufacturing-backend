using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGarmentService
    { 
        public Task<List<Garment>> GetGarmentsAsync();
        public Task<Garment?> GetByIdAsync(int id);
        public Task<Garment> AddAsync(AddGarmentDTO addGarmentDTO);
        public Task<Garment?> UpdateAsync(int id);
        public Task DeleteAsync(int id);

    }


}
