using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;

    public OperationService(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<OperationDTO> AddAsync(AddOperationDTO addOperationDTO)
    {
        var operation = OperationMapper.ToEntity(addOperationDTO);
        var response = await _operationRepository.AddAsync(operation);
        return OperationMapper.ToDto(response);
    }

    public async Task<List<OperationDTO>> GetAllAsync()
    {
        var operations = await _operationRepository.GetAllAsync();
        return operations.Select(OperationMapper.ToDto).ToList();
    }

    public async Task<OperationDTO> GetByIdAsync(int id)
    {
        var operation = await _operationRepository.GetByIdAsync(id);
        if (operation == null)
            throw new Exception($"Operation with id {id} not found.");
        return OperationMapper.ToDto(operation);
    }

    public async Task DeleteAsync(int id)
    {
        await _operationRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(int id, UpdateOperationDTO updateOperationDTO)
    {
        var operation = await _operationRepository.GetByIdAsync(id);
        if (operation == null)
            throw new Exception($"Operation with id {id} not found.");

        operation.OperationName = updateOperationDTO.OperationName;
        operation.OperationDescription = updateOperationDTO.OperationDescription;
        operation.BaseTime = updateOperationDTO.BaseTime;
        operation.UnitsPerGarment = updateOperationDTO.UnitsPerGarment;

        await _operationRepository.UpdateAsync(operation);
    }
}
