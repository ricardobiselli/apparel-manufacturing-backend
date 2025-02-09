using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Models;
using Application.Models.Requests;

namespace Application.Services
{
    public class GarmentService : IGarmentService
    {
        private readonly IGarmentRepository _garmentRepository;

        public GarmentService(IGarmentRepository garmentRepository)
        {
            _garmentRepository = garmentRepository;
        }

        public async Task<List<Garment>> GetGarmentsAsync()
        {
            return await _garmentRepository.GetAllGarmentsWithOperationsIncludedAsync();

        }

        public async Task<Garment?> GetByIdAsync(int id)
        {
            return await _garmentRepository.GetByIdAsync(id);
        }

        public async Task<Garment> AddAsync(AddGarmentDTO addGarmentDTO)
        {
            var garment = new Garment
            (
                addGarmentDTO.GarmentName,
                addGarmentDTO.GarmentDescription,
              addGarmentDTO.Operations.Select(op => new Operation(
                    op.OperationName,
                    op.OperationDescription,
                    op.TimeAllowed)).ToList());

            await _garmentRepository.AddAsync(garment);
            return garment;
        }

        public async Task<Garment?> UpdateAsync(int id)
        {
            return await _garmentRepository.UpdateAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _garmentRepository.DeleteAsync(id);
        }
    }
}

