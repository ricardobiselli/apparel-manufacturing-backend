using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;

//note: handle nulls explicitly in the service
namespace Application.Services;

public class OperationLogService : IOperationLogService
{
    private readonly IOperationLogRepository _operationLogRepository;
    private readonly IMachineSessionRepository _machineSessionRepository;
    private readonly IOrderRepository _orderRepository;

    public OperationLogService(IOperationLogRepository operationLogRepository, IMachineSessionRepository machineSessionRepository, IOrderRepository orderRepository)
    {
        _operationLogRepository = operationLogRepository;
        _machineSessionRepository = machineSessionRepository;
        _orderRepository = orderRepository;
    }

    public async Task<OperationLogDTO> AddOperationLogAsync(AddOperationLogDTO addOperationLogDTO)
    {
        var currentMachineSession = await _machineSessionRepository.GetByIdAsyncIncludingLogs(addOperationLogDTO.MachineSessionId);
        if (currentMachineSession == null)
            throw new KeyNotFoundException($"Machine session {addOperationLogDTO.MachineSessionId} not found.");

        if (currentMachineSession.Status == MachineSessionStatus.Completed)
            throw new InvalidOperationException("Cannot add an operation log to a completed machine session.");

        if (currentMachineSession.Events.Count == 0)
        {
            currentMachineSession.Status = MachineSessionStatus.InProgress;
        }

        var operationLog = OperationLogMapper.ToEntity(addOperationLogDTO);
        var result = await _operationLogRepository.AddOperationLogAsync(operationLog);
        return OperationLogMapper.ToDto(result);
    }

    public async Task<MachineExceptionLogDTO> AddMachineExceptionLogAsync(AddMachineExceptionLogDTO addMachineExceptionLogDTO)
    {
        var currentMachineSession = await _machineSessionRepository.GetByIdAsync(addMachineExceptionLogDTO.MachineSessionId);
        if (currentMachineSession == null)
            throw new KeyNotFoundException($"Machine session {addMachineExceptionLogDTO.MachineSessionId} not found.");

        if (currentMachineSession.Status == MachineSessionStatus.Completed)
            throw new InvalidOperationException("Cannot add an exception log to a completed machine session.");
        
        if (currentMachineSession.Events.Count == 0)
        {
            currentMachineSession.Status = MachineSessionStatus.InProgress;
        }

        var machineExceptionLog = MachineExceptionLogMapper.ToEntity(addMachineExceptionLogDTO);

        if (machineExceptionLog.Type == MachineExceptionType.EndOfProduction)
        {
            currentMachineSession.EndedAt = DateTime.UtcNow;
            currentMachineSession.Status = MachineSessionStatus.Completed;
            await _machineSessionRepository.UpdateAsync(currentMachineSession);

            var currentOrder = await _orderRepository.GetByIdAsync(currentMachineSession.OrderId);
            if (currentOrder == null)
                throw new KeyNotFoundException($"Order {currentMachineSession.OrderId} not found.");

            if (currentOrder.MachineSessions.All(ms => ms.Status == MachineSessionStatus.Completed))
            {
                currentOrder.Status = OrderStatus.Completed;
                await _orderRepository.UpdateAsync(currentOrder);
            }
        }

        var response = await _operationLogRepository.AddMachineExceptionLogAsync(machineExceptionLog);
        return MachineExceptionLogMapper.ToDto(response);
    }
    public async Task<List<OperationLogDTO>> GetAllOperationLogsAsync()
    {
        var logs = await _operationLogRepository.GetAllOperationLogsAsync();
        return logs.Select(OperationLogMapper.ToDto).ToList();
    }

    public async Task<List<MachineExceptionLogDTO>> GetAllExceptionLogsAsync()
    {
        var logs = await _operationLogRepository.GetAllExceptionLogsAsync();
        return logs.Select(MachineExceptionLogMapper.ToDto).ToList();
    }
    public async Task<OperationLogDTO> GetOperationLogByIdAsync(int id)
    {
        var log = await _operationLogRepository.GetOperationLogByIdAsync(id);
        return OperationLogMapper.ToDto(log);
    }

    public async Task<MachineExceptionLogDTO> GetMachineExceptionLogByIdAsync(int id)
    {
        var log = await _operationLogRepository.GetMachineExceptionLogByIdAsync(id);
        return MachineExceptionLogMapper.ToDto(log);
    }

    public async Task DeleteAsync(int id)
    {
        await _operationLogRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(UpdateOperationLogDTO updateOperationLogDto, int id)
    {
        throw new NotImplementedException();
    }

}




