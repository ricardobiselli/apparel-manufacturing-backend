using Application.Models;
using Application.Models.Requests;
using Domain.Models;

namespace Application.Mappers
{

    public static class MachineMapper
    {
        public static Machine ToEntity(AddMachineDTO machineDTO)
        {
            return new Machine(
                  machineDTO.PostNumber,
                  machineDTO.MachineName,
                  machineDTO.MachineModel,
                  machineDTO.PurchaseDate,
                  machineDTO.InstallDate);
        }

        public static MachineDTO ToDto(Machine machine)
        {
            return new MachineDTO
            {
                MachineId = machine.MachineId,
                PostNumber = machine.PostNumber,
                MachineName = machine.MachineName,
                MachineModel = machine.MachineModel,
                PurchaseDate = machine.PurchaseDate,
                InstallDate = machine.InstallDate,
            };
        }

    }
}

