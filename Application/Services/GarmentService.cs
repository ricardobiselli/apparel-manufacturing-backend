using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class GarmentService : IGarmentService
    {
        private readonly IGarmentRepository _garmentRepository;

        public GarmentService(IGarmentRepository garmentRepository)
        {
            _garmentRepository = garmentRepository;
        }

        public async Task<List<GarmentDTO>> GetAllAsync()
        {
            var garments = await _garmentRepository.GetAllGarmentsWithOperationsIncludedAsync();
            var garmentsListDto = garments.Select(GarmentMapper.ToDto).ToList();

            return garmentsListDto;
        }

        public async Task<GarmentDTO> GetByIdAsync(int id)
        {
            var garment = await _garmentRepository.GetByIdAsync(id);
            var garmentDto = GarmentMapper.ToDto(garment);
            return garmentDto;
        }

        public async Task DeleteAsync(int id)
        {

            await _garmentRepository.DeleteAsync(id);
        }

        public async Task<GarmentDTO> AddAsync(CreateGarmentDTO garmentDTO)
        {
            var garment = new Garment
            {
                GarmentName = garmentDTO.GarmentName,
                GarmentDescription = garmentDTO.GarmentDescription,
                Operations = garmentDTO.Operations?.Select(o => new Operation
                {
                    OperationName = o.OperationName,
                    OperationDescription = o.OperationDescription,
                    BaseTime = o.BaseTime
                }).ToList() ?? new List<Operation>()
            };

            var savedGarment = await _garmentRepository.AddAsync(garment);

            return GarmentMapper.ToDto(savedGarment);
        }

        public async Task UpdateAsync(UpdateGarmentDTO updateGarmentDTO, int id)
        {
            throw new NotImplementedException();

        }



    }
}

