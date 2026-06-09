using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MachineSessionRepository : IMachineSessionRepository
    {
        private readonly ApplicationDbContext _context;
        public MachineSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<MachineSession?> GetActiveMachineSessionByMachineId(int machineId)
        {
            return await _context.Set<MachineSession>()
                .Where(ms => ms.MachineId == machineId && ms.Status == MachineSessionStatus.InProgress)
                .SingleOrDefaultAsync();
        }

        public async Task<MachineSession?> GetActiveMachineSessionWithDetailsByMachineId(int machineId)
        {
            return await _context.Set<MachineSession>()
                .Include(ms => ms.Garment)
                .Include(ms => ms.Operation)
                .Where(ms => ms.MachineId == machineId && ms.Status == MachineSessionStatus.InProgress)
                .SingleOrDefaultAsync();
        }

     
        public async Task<List<MachineSession>> GetPendingSessionsForActiveOrdersByMachineId(int machineId)
        {
            return await _context.Set<MachineSession>()
                .Include(ms => ms.Garment)
                .Include(ms => ms.Operation)
                .Where(ms =>
                    ms.MachineId == machineId &&
                    ms.Order.Status == OrderStatus.Active &&
                    ms.Status == MachineSessionStatus.Pending)
                .ToListAsync();
        }


        public async Task<List<MachineSession>> GetAllAsync()
        {
            return await _context.Set<MachineSession>()
            .Include(ms => ms.Garment)
            .Include(ms => ms.Operation)
            .ToListAsync();
        }

        public async Task<MachineSession?> GetByIdAsync(int id)
        {
            return await _context.Set<MachineSession>().FindAsync(id);
        }

        public async Task<MachineSession?> GetByIdAsyncIncludingLogs(int id)
        {
            return await _context.Set<MachineSession>()
                .Include(e => e.Events)
                .SingleOrDefaultAsync(ms => ms.MachineSessionId == id);
        }

        public async Task<MachineSession?> GetByIdWithDetails(int id)
        {
            return await _context.Set<MachineSession>()
                .Include(ms => ms.Garment)
                .Include(ms => ms.Operation)
                .FirstOrDefaultAsync(ms => ms.MachineSessionId == id);
        }

        public async Task<MachineSession> AddAsync(MachineSession entity)
        {
            await _context.Set<MachineSession>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<MachineSession>().FindAsync(id);

            _context.Set<MachineSession>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<MachineSession> UpdateAsync(MachineSession entity)
        {
            _context.Set<MachineSession>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}



