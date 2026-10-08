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
    public class BundleService : IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly ICutBatchRepository _cutBatchRepository;

        public BundleService(IBundleRepository bundleRepository, ICutBatchRepository cutBatchRepository)
        {
            _bundleRepository = bundleRepository;
            _cutBatchRepository = cutBatchRepository;
        }

        public async Task<BundleDTO> AddAsync(CreateBundleDTO dto)
        {
            var cutBatch = await _cutBatch_repository_get(dto.CutBatchId);

            if (!cutBatch.ActualQuantity.HasValue)
                throw new ServiceException("Bundles can only be created from actual cutting quantities.");

            var cutBatchSizesById = cutBatch.Sizes.ToDictionary(s => s.SizeId);

            var totalBundleQty = dto.Sizes.Sum(s => s.Quantity);
            if (totalBundleQty <= 0) throw new ServiceException("Bundle.Quantity must be greater than zero and equal to sum of bundle sizes.");

            // existing bundled totals
            var existingBundleSums = cutBatch.Bundles
                .SelectMany(b => b.Sizes)
                .GroupBy(bs => bs.SizeId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

            foreach (var s in dto.Sizes)
            {
                if (!cutBatchSizesById.ContainsKey(s.SizeId))
                    throw new ServiceException($"Size {s.SizeId} is not part of the cut batch.");

                var cutActual = cutBatchSizesById[s.SizeId].ActualQuantity ?? 0;
                var alreadyBundled = existingBundleSums.ContainsKey(s.SizeId) ? existingBundleSums[s.SizeId] : 0;
                if (s.Quantity < 0)
                    throw new ServiceException("Bundle size quantities cannot be negative.");

                if (alreadyBundled + s.Quantity > cutActual)
                    throw new ServiceException($"Total bundle quantity for size {s.SizeId} would exceed cut batch actual quantity.");
            }

            var existingTotalBundled = cutBatch.Bundles.Sum(b => b.Quantity);
            if (existingTotalBundled + totalBundleQty > cutBatch.ActualQuantity)
                throw new ServiceException("Total bundled quantity cannot exceed CutBatch.ActualQuantity.");

            var entity = new Bundle
            {
                CutBatchId = dto.CutBatchId,
                Quantity = totalBundleQty,
                Status = Domain.Enums.BundleStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Sizes = dto.Sizes.Select(s => new BundleSize
                {
                    SizeId = s.SizeId,
                    Quantity = s.Quantity
                }).ToList()
            };

            var saved = await _bundleRepository.AddAsync(entity);
            var full = await _bundleRepository.GetByIdWithSizesAsync(saved.BundleId);
            return BundleMapper.ToDto(full);
        }

        public async Task<List<BundleDTO>> GetAllAsync()
        {
            var list = await _bundleRepository.GetAllAsync();
            return list.Select(BundleMapper.ToDto).ToList();
        }

        public async Task<BundleDTO> GetByIdAsync(int id)
        {
            var entity = await _bundleRepository.GetByIdWithSizesAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Bundle with id {id} not found.");
            return BundleMapper.ToDto(entity);
        }

        public async Task<List<BundleDTO>> GetByCutBatchIdAsync(int cutBatchId)
        {
            var list = await _bundleRepository.GetByCutBatchIdAsync(cutBatchId);
            return list.Select(BundleMapper.ToDto).ToList();
        }

        public async Task UpdateAsync(CreateBundleDTO dto, int id)
        {
            var existing = await _bundle_repository_get_by_id(id);
            if (existing == null) throw new KeyNotFoundException($"Bundle with id {id} not found.");

            var cutBatch = await _cutBatch_repository_get(existing.CutBatchId);

            // remove this bundle from consideration when validating
            var otherBundles = cutBatch.Bundles.Where(b => b.BundleId != existing.BundleId).ToList();

            var cutBatchSizesById = cutBatch.Sizes.ToDictionary(s => s.SizeId);
            var totalBundleQty = dto.Sizes.Sum(s => s.Quantity);
            if (totalBundleQty <= 0) throw new ServiceException("Bundle.Quantity must be greater than zero.");

            var existingBundleSums = otherBundles
                .SelectMany(b => b.Sizes)
                .GroupBy(bs => bs.SizeId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

            foreach (var s in dto.Sizes)
            {
                if (!cutBatchSizesById.ContainsKey(s.SizeId))
                    throw new ServiceException($"Size {s.SizeId} is not part of the cut batch.");

                var cutActual = cutBatchSizesById[s.SizeId].ActualQuantity ?? 0;
                var alreadyBundled = existingBundleSums.ContainsKey(s.SizeId) ? existingBundleSums[s.SizeId] : 0;
                if (alreadyBundled + s.Quantity > cutActual)
                    throw new ServiceException($"Total bundle quantity for size {s.SizeId} would exceed cut batch actual quantity.");
            }

            var existingTotalBundled = otherBundles.Sum(b => b.Quantity);
            if (existingTotalBundled + totalBundleQty > cutBatch.ActualQuantity)
                throw new ServiceException("Total bundled quantity cannot exceed CutBatch.ActualQuantity.");

            // Sync sizes in-place on the existing bundle
            var incoming = dto.Sizes.ToDictionary(s => s.SizeId);

            var toRemove = existing.Sizes.Where(es => !incoming.ContainsKey(es.SizeId)).ToList();
            foreach (var r in toRemove)
                existing.Sizes.Remove(r);

            foreach (var es in existing.Sizes)
            {
                if (incoming.TryGetValue(es.SizeId, out var inc))
                    es.Quantity = inc.Quantity;
            }

            var existingNow = existing.Sizes.Select(s => s.SizeId).ToHashSet();
            var toAdd = dto.Sizes.Where(s => !existingNow.Contains(s.SizeId))
                .Select(s => new BundleSize { BundleId = existing.BundleId, SizeId = s.SizeId, Quantity = s.Quantity }).ToList();

            foreach (var a in toAdd) existing.Sizes.Add(a);

            existing.Quantity = existing.Sizes.Sum(s => s.Quantity);

            await _bundleRepository.UpdateAsync(existing);
        }

        private async Task<CutBatch> _cutBatch_repository_get(int cutBatchId)
        {
            var cb = await _cutBatchRepository.GetByIdWithSizesAndBundlesAsync(cutBatchId);
            if (cb == null) throw new ServiceException($"CutBatch {cutBatchId} not found.");
            return cb;
        }

        private async Task<Bundle> _bundle_repository_get_by_id(int bundleId)
        {
            var b = await _bundleRepository.GetByIdWithSizesAsync(bundleId);
            return b;
        }
    }
}
