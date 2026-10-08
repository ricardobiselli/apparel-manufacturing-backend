using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BundleRepository : IBundleRepository
    {
        private readonly ApplicationDbContext _context;

        public BundleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bundle>> GetAllAsync()
        {
            return await _context.Set<Bundle>()
                .Include(b => b.Sizes)
                    .ThenInclude(bs => bs.Size)
                .Include(b => b.CutBatch)
                .ToListAsync();
        }

        public async Task<Bundle?> GetByIdWithSizesAsync(int id)
        {
            return await _context.Set<Bundle>()
                .Include(b => b.Sizes)
                    .ThenInclude(bs => bs.Size)
                .Include(b => b.CutBatch)
                .FirstOrDefaultAsync(b => b.BundleId == id);
        }

        public async Task<List<Bundle>> GetByCutBatchIdAsync(int cutBatchId)
        {
            return await _context.Set<Bundle>()
                .Where(b => b.CutBatchId == cutBatchId)
                .Include(b => b.Sizes)
                    .ThenInclude(bs => bs.Size)
                .ToListAsync();
        }

        public async Task<Bundle> AddAsync(Bundle entity)
        {
            await _context.Set<Bundle>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Bundle entity)
        {
            _context.Set<Bundle>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Bundle>().FindAsync(id);
            if (entity == null) return;
            _context.Set<Bundle>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}