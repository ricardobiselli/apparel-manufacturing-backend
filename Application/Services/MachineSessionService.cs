using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class MachineSessionService : IMachineSessionService
{

    private readonly IMachineSessionRepository _machineSessionRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOperationRepository _operationRepository;
    public MachineSessionService(
        IMachineSessionRepository machineSessionRepository,
        IOrderRepository orderRepository,
        IOperationRepository operationRepository)
    {
        _machineSessionRepository = machineSessionRepository;
        _orderRepository = orderRepository;
        _operationRepository = operationRepository;
    }
    public async Task<MachineSessionDTO> AddAsync(AddMachineSessionDTO dto)
    {
        var operation = await _operationRepository.GetByIdAsync(dto.OperationId);

        if (operation == null)
        {
            throw new Exception($"Operation with id {dto.OperationId} not found.");
        }

        var machineSession = new MachineSession
        {
            OrderId = dto.OrderId,
            MachineId = dto.MachineId,
            GarmentId = dto.GarmentId,
            OperationId = operation.OperationId,
            Status = dto.Status,

            // Snapshot
            OperationName = operation.OperationName,
            OperationDescription = operation.OperationDescription,
            BaseTime = operation.BaseTime,
            UnitsPerGarment = operation.UnitsPerGarment
        };

        await _machineSessionRepository.AddAsync(machineSession);

        var fullEntity =
            await _machineSessionRepository.GetByIdWithDetails(machineSession.MachineSessionId);

        return MachineSessionMapper.ToDto(fullEntity);
    }

    public async Task<List<MachineSessionDTO>> GetAllAsync()
    {
        var machineSessionList = await _machineSessionRepository.GetAllAsync();
        var machineSessionListDto = machineSessionList
            .Select(MachineSessionMapper.ToDto)
            .ToList();
        return machineSessionListDto;
    }
    public async Task<MachineSessionDTO> GetByIdAsync(int id)
    {
        var machineSession = await _machineSessionRepository.GetByIdAsync(id);
        var MachineSessionDTO = MachineSessionMapper.ToDto(machineSession);
        return MachineSessionDTO;
    }

    public async Task<MachineSessionDTO> GetByIdWithDetailsAsync(int id)
    {
        var machineSession = await _machineSessionRepository.GetByIdWithDetails(id);
        var MachineSessionDTO = MachineSessionMapper.ToDto(machineSession);
        return MachineSessionDTO;
    }

    public async Task<MachineSessionDTO> GetActiveMachineSessionByMachineId(int id)
    {
        var machineSession = await _machineSessionRepository.GetActiveMachineSessionByMachineId(id);
        var MachineSessionDTO = MachineSessionMapper.ToDto(machineSession);
        return MachineSessionDTO;
    }

    public async Task<MachineSessionDTO> GetActiveMachineSessionByMachineIdWithDetailsIncluded(int id)
    {
        var session = await _machineSessionRepository
            .GetActiveMachineSessionWithDetailsByMachineId(id);
        if (session == null)
            return null;
        return MachineSessionMapper.ToDto(session);
    }

    public async Task DeleteAsync(int id)
    {
        await _machineSessionRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(UpdateMachineSessionDTO dto, int id)
    {
        var existing = await _machineSessionRepository.GetByIdAsync(id);

        if (existing == null)
        {
            throw new KeyNotFoundException(
                $"MachineSession with id {id} not found.");
        }

        // Update assigned operator
        if (dto.UserId.HasValue)
        {
            existing.UserId = dto.UserId.Value;
        }

        // Handle status transitions
        if (dto.Status.HasValue)
        {
            var newStatus = dto.Status.Value;

            if (newStatus == MachineSessionStatus.InProgress)
            {
                if (existing.StartedAt == null)
                {
                    existing.StartedAt = DateTime.UtcNow;
                }
            }
            else if (newStatus == MachineSessionStatus.Completed)
            {
                if (existing.StartedAt == null)
                {
                    throw new InvalidOperationException(
                        "Cannot complete a machine session that has not started.");
                }

                if (existing.EndedAt == null)
                {
                    existing.EndedAt = DateTime.UtcNow;
                }
            }

            existing.Status = newStatus;
        }

        // Update operation snapshot fields
        if (dto.OperationName != null)
        {
            existing.OperationName = dto.OperationName;
        }

        if (dto.OperationDescription != null)
        {
            existing.OperationDescription = dto.OperationDescription;
        }

        if (dto.BaseTime.HasValue)
        {
            existing.BaseTime = dto.BaseTime.Value;
        }

        if (dto.UnitsPerGarment.HasValue)
        {
            existing.UnitsPerGarment = dto.UnitsPerGarment.Value;
        }

        await _machineSessionRepository.UpdateAsync(existing);
    }


    public async Task<ICollection<MachineSessionDTO>> GetAllSessionsExceptPendingOrInProgressByMachineId(int machineId)
    {
        var machineSessions = await _machineSessionRepository.GetAllSessionsExceptPendingOrInProgressByMachineId(machineId);
        var machineSessionDtos = machineSessions.Select(MachineSessionMapper.ToDto).ToList();
        return machineSessionDtos;
    }

}