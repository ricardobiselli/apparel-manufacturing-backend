using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _context;

        public MachineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Machine>> GetAllAsync()
        {
            return await _context.Set<Machine>()
                .ToListAsync();
        }

        public async Task<Machine?> GetByIdAsync(int id)
        {
            return await _context.Set<Machine>()
                .FirstOrDefaultAsync(m => m.MachineId == id);
        }

        public async Task<Machine> AddAsync(Machine entity)
        {
            await _context.Set<Machine>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Machine entity)
        {
            _context.Set<Machine>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Machine>().FindAsync(id);

            if (entity == null)
                return;

            _context.Set<Machine>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Machine?> GetByPostNumberAsync(int postNumber)
        {
            return await _context.Set<Machine>()
                .FirstOrDefaultAsync(m => m.PostNumber == postNumber);
        }

    }
}
