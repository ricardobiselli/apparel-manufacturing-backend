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

    public OperationLogService(IOperationLogRepository operationLogRepository, IMachineSessionRepository machineSessionRepository)
    {
        _operationLogRepository = operationLogRepository;
        _machineSessionRepository = machineSessionRepository;
    }

    public async Task<OperationLogDTO> AddOperationLogAsync(AddOperationLogDTO addOperationLogDTO)
    {
        var lastLog =
            await _operationLogRepository
     .GetLastByMachineSessionIdAsync(addOperationLogDTO.MachineSessionId);
        var operationLog = OperationLogMapper.ToEntity(addOperationLogDTO);
        var result = await _operationLogRepository.AddOperationLogAsync(operationLog);
        return OperationLogMapper.ToDto(result);
    }

    public async Task<MachineExceptionLogDTO> AddMachineExceptionLogAsync(AddMachineExceptionLogDTO addMachineExceptionLogDTO)
    {
        var exceptionType = addMachineExceptionLogDTO.Type;
        Console.WriteLine($"DTO TYPE NULL?: {addMachineExceptionLogDTO.Type}");
        Console.WriteLine($"DTO TYPE INT: {(int)addMachineExceptionLogDTO.Type}");


        var machineExceptionLog = MachineExceptionLogMapper.ToEntity(addMachineExceptionLogDTO);
        machineExceptionLog.Type = exceptionType;

        if (machineExceptionLog.Type == MachineExceptionType.EndOfProduction)
        {
            var currentMachineSession = await _machineSessionRepository.GetByIdAsync(addMachineExceptionLogDTO.MachineSessionId);
            currentMachineSession.EndedAt = DateTime.UtcNow;
            currentMachineSession.Status = MachineSessionStatus.Completed;
            await _machineSessionRepository.UpdateAsync(currentMachineSession);
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




