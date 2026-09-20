using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FabricRollService : IFabricRollService
    {
        private readonly IFabricRollRepository _fabricRollRepository;

        public FabricRollService(IFabricRollRepository fabricRollRepository)
        {
            _fabricRollRepository = fabricRollRepository;
        }

        public async Task<List<FabricRollDTO>> GetAllAsync()
        {
            var rolls = await _fabricRollRepository.GetAllAsync();
            return rolls.Select(FabricRollMapper.ToDto).ToList();
        }

        public async Task<FabricRollDTO> GetByIdAsync(int id)
        {
            var roll = await _fabricRollRepository.GetByIdAsync(id);
            if (roll == null) throw new KeyNotFoundException($"FabricRoll with id {id} not found.");
            return FabricRollMapper.ToDto(roll);
        }

        public async Task<FabricRollDTO> AddAsync(CreateFabricRollDTO createFabricRollDTO)
        {
            var entity = new FabricRoll
            {
                FabricRollName = createFabricRollDTO.FabricRollName,
                Color = createFabricRollDTO.Color,
                FabricRollDescription = createFabricRollDTO.FabricRollDescription,
                WeightOrLength = createFabricRollDTO.WeightOrLength,
                Yield = createFabricRollDTO.Yield,
                Date = createFabricRollDTO.Date ?? DateOnly.FromDateTime(DateTime.UtcNow),
                BarCode = createFabricRollDTO.BarCode
            };

            var saved = await _fabricRollRepository.AddAsync(entity);
            return FabricRollMapper.ToDto(saved);
        }

        public async Task DeleteAsync(int id)
        {
            await _fabricRollRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UpdateFabricRollDTO updateFabricRollDTO, int id)
        {
            var existing = await _fabricRollRepository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException($"FabricRoll with id {id} not found.");

            existing.FabricRollName = updateFabricRollDTO.FabricRollName ?? existing.FabricRollName;
            existing.Color = updateFabricRollDTO.Color ?? existing.Color;
            existing.FabricRollDescription = updateFabricRollDTO.FabricRollDescription ?? existing.FabricRollDescription;
            if (updateFabricRollDTO.WeightOrLength.HasValue) existing.WeightOrLength = updateFabricRollDTO.WeightOrLength.Value;
            if (updateFabricRollDTO.Yield.HasValue) existing.Yield = updateFabricRollDTO.Yield.Value;
            if (updateFabricRollDTO.Date.HasValue) existing.Date = updateFabricRollDTO.Date.Value;
            if (updateFabricRollDTO.BarCode.HasValue) existing.BarCode = updateFabricRollDTO.BarCode;

            await _fabricRollRepository.UpdateAsync(existing);
        }
    }
}