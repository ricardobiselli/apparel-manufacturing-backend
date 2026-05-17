using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GarmentRepository : IGarmentRepository
    {
        private readonly ApplicationDbContext _context;

        public GarmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Garment>> GetAllAsync()
        {
            return await _context.Set<Garment>()
                .ToListAsync();
        }

        public async Task<List<Garment>> GetAllGarmentsWithOperationsIncludedAsync()
        {
            return await _context.Set<Garment>()
                .Include(g => g.Operations)
                .ToListAsync();
        }

        public async Task<Garment?> GetByIdAsync(int id)
        {
            return await _context.Set<Garment>()
                .FirstOrDefaultAsync(g => g.GarmentId == id);
        }

        public async Task<Garment?> GetByIdWithOperationsAsync(int id)
        {
            return await _context.Set<Garment>()
                .Include(g => g.Operations)
                .FirstOrDefaultAsync(g => g.GarmentId == id);
        }

        public async Task<Garment> AddAsync(Garment entity)
        {
            await _context.Set<Garment>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Garment entity)
        {
            _context.Set<Garment>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Garment>().FindAsync(id);

            if (entity == null)
                return;

            _context.Set<Garment>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
