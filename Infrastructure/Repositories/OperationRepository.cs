using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Operation>> GetAllAsync()
        {
            return await _context.Set<Operation>()
                .ToListAsync();
        }

        public async Task<Operation?> GetByIdAsync(int id)
        {
            return await _context.Set<Operation>()
                .FirstOrDefaultAsync(o => o.OperationId == id);
        }

        public async Task<Operation> AddAsync(Operation entity)
        {
            await _context.Set<Operation>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Operation entity)
        {
            _context.Set<Operation>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Operation>().FindAsync(id);

            if (entity == null)
                return;

            _context.Set<Operation>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}