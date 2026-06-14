using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;

namespace Application.Services;

public class MachineSessionService : IMachineSessionService
{

    private readonly IMachineSessionRepository _machineSessionRepository;
    private readonly IOrderRepository _orderRepository;

    public MachineSessionService(IMachineSessionRepository machineSessionRepository, IOrderRepository orderRepository)
    {
        _machineSessionRepository = machineSessionRepository;
        _orderRepository = orderRepository;
    }

    public async Task<MachineSessionDTO> AddAsync(AddMachineSessionDTO dto)
    {
        var machineSessionEntity = MachineSessionMapper.ToEntity(dto);

        await _machineSessionRepository.AddAsync(machineSessionEntity);

        //var order = await _orderRepository.GetByIdAsync(machineSessionEntity.OrderId);
        //if (order.Status == OrderStatus.Active)
        //{
        //    machineSessionEntity.Status = MachineSessionStatus.InProgress;
        //    await _machineSessionRepository.UpdateAsync(machineSessionEntity);
        //}

        var fullEntity =
            await _machineSessionRepository
                .GetByIdWithDetails(machineSessionEntity.MachineSessionId);

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
        throw new NotImplementedException();
    }

    public async Task<ICollection<MachineSessionDTO>> GetPendingSessionsForActiveOrdersByMachineId(int machineId)
    {
        var machineSessions = await _machineSessionRepository.GetPendingSessionsForActiveOrdersByMachineId(machineId);
        var machineSessionDtos = machineSessions.Select(MachineSessionMapper.ToDto).ToList();
        return machineSessionDtos;
    }

}