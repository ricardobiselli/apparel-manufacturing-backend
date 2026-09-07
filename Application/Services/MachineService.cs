using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Requests;
using Domain.Exceptions;
using Domain.IRepositories;
using System.ComponentModel.DataAnnotations;



namespace Application.Services
{
    public class MachineService : IMachineService

    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public async Task<MachineDTO> AddAsync(AddMachineDTO addMachineDTO)
        {
            var existingPostNumber = await _machineRepository
                .GetByPostNumberAsync(addMachineDTO.PostNumber);

            if (existingPostNumber != null)
            {
                throw new ValidateException(
                    $"Postnumber already exists:  {addMachineDTO.PostNumber}.");
            }

            var machine = MachineMapper.ToEntity(addMachineDTO);
            var response = await _machineRepository.AddAsync(machine);
            var machineDto = MachineMapper.ToDto(response);
            return machineDto;
        }
        public async Task<List<MachineDTO>> GetAllAsync()
        {
            var machineList = await _machineRepository.GetAllAsync();
            var machineListDto = machineList
                .Select(MachineMapper.ToDto)
                .ToList();
            return machineListDto;
        }
        public async Task<MachineDTO> GetByIdAsync(int id)
        {
            var machine = await _machineRepository.GetByIdAsync(id);
            var machineDto = MachineMapper.ToDto(machine);
            return machineDto;
        }
        public async Task DeleteAsync(int id)
        {
            await _machineRepository.DeleteAsync(id);
        }
        public async Task UpdateAsync(int id, UpdateMachineDTO updateMachineDTO)
        {
            var machine = await _machineRepository.GetByIdAsync(id);

            if (machine == null)
            {
                throw new NotFoundException(
                    $"Machine with id {id} not found.");
            }

            var existingPostNumber = await _machineRepository
                .GetByPostNumberAsync(updateMachineDTO.PostNumber);

            if (existingPostNumber != null && existingPostNumber.MachineId != id)
            {
                throw new ValidationException(
                    $"Post number {updateMachineDTO.PostNumber} is already in use.");
            }

            MachineMapper.UpdateEntity(machine, updateMachineDTO);

            await _machineRepository.UpdateAsync(machine);
        }
    }

}

