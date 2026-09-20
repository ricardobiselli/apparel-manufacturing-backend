using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FabricRollRepository : IFabricRollRepository
    {
        private readonly ApplicationDbContext _context;

        public FabricRollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FabricRoll>> GetAllAsync()
        {
            return await _context.Set<FabricRoll>().ToListAsync();
        }

        public async Task<FabricRoll?> GetByIdAsync(int id)
        {
            return await _context.Set<FabricRoll>().FirstOrDefaultAsync(fr => fr.FabricRollId == id);
        }

        public async Task<FabricRoll> AddAsync(FabricRoll entity)
        {
            await _context.Set<FabricRoll>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(FabricRoll entity)
        {
            _context.Set<FabricRoll>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<FabricRoll>().FindAsync(id);
            if (entity == null) return;
            _context.Set<FabricRoll>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}