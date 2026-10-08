using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CutBatchRepository : ICutBatchRepository
    {
        private readonly ApplicationDbContext _context;

        public CutBatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CutBatch>> GetAllWithSizesAsync()
        {
            return await _context.Set<CutBatch>()
                .Include(cb => cb.Sizes)
                    .ThenInclude(s => s.Size)
                .Include(cb => cb.Bundles)
                    .ThenInclude(b => b.Sizes)
                        .ThenInclude(bs => bs.Size)
                .ToListAsync();
        }

        public async Task<CutBatch?> GetByIdAsync(int id)
        {
            return await _context.Set<CutBatch>()
                .FirstOrDefaultAsync(cb => cb.CutBatchId == id);
        }

        public async Task<CutBatch?> GetByIdWithSizesAndBundlesAsync(int id)
        {
            return await _context.Set<CutBatch>()
                .Include(cb => cb.Sizes)
                    .ThenInclude(s => s.Size)
                .Include(cb => cb.Bundles)
                    .ThenInclude(b => b.Sizes)
                .FirstOrDefaultAsync(cb => cb.CutBatchId == id);
        }

        public async Task<List<CutBatch>> GetByOrderIdAsync(int orderId)
        {
            return await _context.Set<CutBatch>()
                .Where(cb => cb.OrderId == orderId)
                .Include(cb => cb.Sizes)
                    .ThenInclude(s => s.Size)
                .ToListAsync();
        }

        public async Task<CutBatch> AddAsync(CutBatch entity)
        {
            await _context.Set<CutBatch>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(CutBatch entity)
        {
            _context.Set<CutBatch>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<CutBatch>().FindAsync(id);
            if (entity == null) return;
            _context.Set<CutBatch>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}