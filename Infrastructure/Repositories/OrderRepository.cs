using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Set<Order>()
                .Include(o => o.OrderGarments)
                .ThenInclude(og => og.Garment)
                .ThenInclude(g => g.Operations)
                .Include(o => o.MachineSessions)
                    .ThenInclude(ms => ms.Garment)
                .Include(o => o.MachineSessions)
                    //.ThenInclude(ms => ms.Operation)
                .ToListAsync();
        }


        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Set<Order>()
                .Include(o => o.OrderGarments)
                    .ThenInclude(og => og.Garment)
                        .ThenInclude(g => g.Operations)
                .Include(o => o.MachineSessions)
                    .ThenInclude(ms => ms.Garment)
                .Include(o => o.MachineSessions)
                    //.ThenInclude(ms => ms.Operation)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> AddAsync(Order entity)
        {
            await _context.Set<Order>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Order>().FindAsync(id);

            if (entity == null)
                return;

            _context.Set<Order>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            //_context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
