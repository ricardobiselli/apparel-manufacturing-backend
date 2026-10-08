using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CutBatchService : ICutBatchService
    {
        private readonly ICutBatchRepository _cutBatchRepository;
        private readonly IOrderRepository _orderRepository;

        public CutBatchService(ICutBatchRepository cutBatchRepository, IOrderRepository orderRepository)
        {
            _cutBatchRepository = cutBatchRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CutBatchDTO> AddAsync(CreateCutBatchDTO dto)
        {
            var sumPlanned = dto.Sizes.Sum(s => s.PlannedQuantity);
            if (sumPlanned != dto.PlannedQuantity)
                throw new ServiceException("CutBatch.PlannedQuantity must equal the sum of sizes PlannedQuantity.");

            var order = await _orderRepository.GetByIdAsync(dto.OrderId);
            if (order == null)
                throw new ServiceException($"Order {dto.OrderId} not found.");

            var orderGarment = order.OrderGarments.FirstOrDefault(og => og.GarmentId == dto.GarmentId);
            if (orderGarment == null)
                throw new ServiceException($"OrderGarment for garment {dto.GarmentId} not found in order {dto.OrderId}.");

            var allowedSizeIds = orderGarment.Sizes.Select(s => s.SizeId).ToHashSet();
            foreach (var s in dto.Sizes)
            {
                if (!allowedSizeIds.Contains(s.SizeId))
                    throw new ServiceException($"Size {s.SizeId} is not part of the order garment.");
            }

            var entity = new CutBatch
            {
                OrderId = dto.OrderId,
                GarmentId = dto.GarmentId,
                PlannedQuantity = dto.PlannedQuantity,
                CreatedAt = DateTime.UtcNow,
                Sizes = dto.Sizes.Select(s => new CutBatchSize
                {
                    SizeId = s.SizeId,
                    PlannedQuantity = s.PlannedQuantity
                }).ToList()
            };

            var saved = await _cutBatchRepository.AddAsync(entity);
            var full = await _cutBatchRepository.GetByIdWithSizesAndBundlesAsync(saved.CutBatchId);
            return CutBatchMapper.ToDto(full);
        }

        public async Task<List<CutBatchDTO>> GetAllAsync()
        {
            var list = await _cutBatchRepository.GetAllWithSizesAsync();
            return list.Select(CutBatchMapper.ToDto).ToList();
        }

        public async Task<CutBatchDTO> GetByIdAsync(int id)
        {
            var entity = await _cutBatchRepository.GetByIdWithSizesAndBundlesAsync(id);
            if (entity == null) throw new KeyNotFoundException($"CutBatch with id {id} not found.");
            return CutBatchMapper.ToDto(entity);
        }

        public async Task UpdateAsync(UpdateCutBatchDTO dto, int id)
        {
            var existing = await _cutBatchRepository.GetByIdWithSizesAndBundlesAsync(id);
            if (existing == null) throw new KeyNotFoundException($"CutBatch with id {id} not found.");

            var sumPlanned = dto.Sizes.Sum(s => s.PlannedQuantity);
            if (sumPlanned != dto.PlannedQuantity)
                throw new ServiceException("CutBatch.PlannedQuantity must equal the sum of sizes PlannedQuantity.");

            var order = await _orderRepository.GetByIdAsync(existing.OrderId);
            if (order == null) throw new ServiceException($"Order {existing.OrderId} not found.");

            var orderGarment = order.OrderGarments.FirstOrDefault(og => og.GarmentId == existing.GarmentId);
            if (orderGarment == null) throw new ServiceException($"OrderGarment for garment {existing.GarmentId} not found.");

            var allowedSizeIds = orderGarment.Sizes.Select(s => s.SizeId).ToHashSet();
            foreach (var s in dto.Sizes)
            {
                if (!allowedSizeIds.Contains(s.SizeId))
                    throw new ServiceException($"Size {s.SizeId} is not part of the order garment.");
            }

            existing.PlannedQuantity = dto.PlannedQuantity;

            var incoming = dto.Sizes.ToDictionary(s => s.SizeId);

            // Update existing sizes in-place and collect which to keep
            var existingSizeIds = existing.Sizes.Select(s => s.SizeId).ToHashSet();

            // Update quantities for existing sizes and remove those not present
            var toRemove = existing.Sizes.Where(es => !incoming.ContainsKey(es.SizeId)).ToList();
            foreach (var r in toRemove)
            {
                existing.Sizes.Remove(r);
            }

            foreach (var es in existing.Sizes)
            {
                if (incoming.TryGetValue(es.SizeId, out var incomingDto))
                    es.PlannedQuantity = incomingDto.PlannedQuantity;
            }

            // Add new sizes
            var existingNow = existing.Sizes.Select(s => s.SizeId).ToHashSet();
            var toAdd = dto.Sizes.Where(s => !existingNow.Contains(s.SizeId))
                .Select(s => new CutBatchSize
                {
                    CutBatchId = existing.CutBatchId,
                    SizeId = s.SizeId,
                    PlannedQuantity = s.PlannedQuantity
                }).ToList();

            foreach (var a in toAdd) existing.Sizes.Add(a);

            await _cutBatch_repository_update(existing);
        }

        private async Task _cutBatch_repository_update(CutBatch existing)
        {
            await _cutBatchRepository.UpdateAsync(existing);
        }

        public async Task RegisterActualsAsync(RegisterCutBatchActualsDTO dto, int id)
        {
            var existing = await _cutBatchRepository.GetByIdWithSizesAndBundlesAsync(id);
            if (existing == null) throw new KeyNotFoundException($"CutBatch with id {id} not found.");

            var batchSizesById = existing.Sizes.ToDictionary(s => s.SizeId);

            foreach (var s in dto.Sizes)
            {
                if (!batchSizesById.ContainsKey(s.SizeId))
                    throw new ServiceException($"Size {s.SizeId} is not part of the cut batch.");

                if (s.ActualQuantity < 0)
                    throw new ServiceException("Actual quantities cannot be negative.");
            }

            foreach (var s in dto.Sizes)
            {
                batchSizesById[s.SizeId].ActualQuantity = s.ActualQuantity;
            }

            existing.ActualQuantity = existing.Sizes.Sum(s => s.ActualQuantity ?? 0);

            await _cutBatchRepository.UpdateAsync(existing);
        }
    }
}
