using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

//review name of this repository later
public class OperationLogRepository : IOperationLogRepository
{
    private readonly ApplicationDbContext _context;

    public OperationLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationLog> AddOperationLogAsync(OperationLog entity)
    {
        await _context.Set<OperationLog>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<MachineExceptionLog> AddMachineExceptionLogAsync(MachineExceptionLog entity)
    {
        await _context.Set<MachineExceptionLog>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<OperationLog>> GetByMachineSessionIdAsync(int machineSessionId)
    {
        return await _context.Set<OperationLog>()
            .Where(log => log.MachineSessionId == machineSessionId)
            .OrderBy(log => log.Timestamp)
            .ToListAsync();
    }

    public async Task<List<OperationLog>> GetByOperationIdAsync(int Id)
    {
        return await _context.Set<OperationLog>()
            .Where(log => log.MachineEventId == Id)
            .OrderBy(log => log.Timestamp)
            .ToListAsync();
    }

    public async Task<OperationLog?> GetLastByMachineSessionIdAsync(int machineSessionId)
    {
        return await _context.Set<OperationLog>()
            .Where(log => log.MachineSessionId == machineSessionId)
            .OrderByDescending(log => log.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<MachineExceptionLog?> GetLastExceptionByMachineSessionIdAsync(int machineSessionId)
    {
        return await _context.Set<MachineExceptionLog>()
            .Where(log => log.MachineSessionId == machineSessionId)
             .OrderByDescending(log => log.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<List<OperationLog>> GetAllOperationLogsAsync()
    {
        return await _context.Set<OperationLog>().ToListAsync();
    }
    public async Task<List<MachineExceptionLog>> GetAllExceptionLogsAsync()
    {
        return await _context.Set<MachineExceptionLog>().ToListAsync();
    }

    public async Task<List<MachineEvent>> GetAllMachineEvents()
    {
        return await _context.Set<MachineEvent>()
                .ToListAsync();
    }

    public async Task<OperationLog?> GetOperationLogByIdAsync(int id)
    {
        return await _context.Set<OperationLog>().FindAsync(id);
    }

    public async Task<MachineExceptionLog?> GetMachineExceptionLogByIdAsync(int id)
    {
        return await _context.Set<MachineExceptionLog>().FindAsync(id);
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<OperationLog>().FindAsync(id);

        _context.Set<OperationLog>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<OperationLog> UpdateAsync(OperationLog entity)
    {
        _context.Set<OperationLog>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}

