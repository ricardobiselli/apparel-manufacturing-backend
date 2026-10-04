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
            //UserId = dto.UserId,
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
    public async Task UpdateAsync(UpdateMachineSessionDTO updateMachineSessionDTO, int id)
    {
        var existing = await _machineSessionRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"MachineSession with id {id} not found.");
        //
        // REVISIT THIS PART LATER:
        //
        // If caller provided a user id (controller will set operator id), set it
        if (updateMachineSessionDTO.UserId.HasValue)
            existing.UserId = updateMachineSessionDTO.UserId.Value;

        // Update status & timestamps with sensible defaults
        if (updateMachineSessionDTO.Status.HasValue)
        {
            var newStatus = updateMachineSessionDTO.Status.Value;

            // When an operator starts the session, set StartedAt if missing
            if (newStatus == MachineSessionStatus.InProgress && existing.StartedAt == null)
            {
                existing.StartedAt = updateMachineSessionDTO.StartedAt ?? DateTime.UtcNow;
            }

            // When marking completed, set EndedAt if missing
            if (newStatus == MachineSessionStatus.Completed && existing.EndedAt == null)
            {
                existing.EndedAt = updateMachineSessionDTO.EndedAt ?? DateTime.UtcNow;
            }

            existing.Status = newStatus;
        }
        else
        {
            // If status not provided, allow updating individual timestamps
            if (updateMachineSessionDTO.StartedAt.HasValue)
                existing.StartedAt = updateMachineSessionDTO.StartedAt.Value;

            if (updateMachineSessionDTO.EndedAt.HasValue)
                existing.EndedAt = updateMachineSessionDTO.EndedAt.Value;
        }

        if (updateMachineSessionDTO.OperationName != null)
            existing.OperationName = updateMachineSessionDTO.OperationName;

        if (updateMachineSessionDTO.OperationDescription != null)
            existing.OperationDescription = updateMachineSessionDTO.OperationDescription;

        if (updateMachineSessionDTO.BaseTime.HasValue)
            existing.BaseTime = updateMachineSessionDTO.BaseTime.Value;

        if (updateMachineSessionDTO.UnitsPerGarment.HasValue)
            existing.UnitsPerGarment = updateMachineSessionDTO.UnitsPerGarment.Value;
        await _machineSessionRepository.UpdateAsync(existing);
    }

    public async Task<ICollection<MachineSessionDTO>> GetAllSessionsExceptPendingOrInProgressByMachineId(int machineId)
    {
        var machineSessions = await _machineSessionRepository.GetAllSessionsExceptPendingOrInProgressByMachineId(machineId);
        var machineSessionDtos = machineSessions.Select(MachineSessionMapper.ToDto).ToList();
        return machineSessionDtos;
    }

}