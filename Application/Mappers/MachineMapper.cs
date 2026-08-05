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
                  machineDTO.InstallDate
                  );
        }

        public static void UpdateEntity(Machine machine, UpdateMachineDTO machineDTO)
        {
            machine.PostNumber = machineDTO.PostNumber;
            machine.MachineName = machineDTO.MachineName;
            machine.MachineModel = machineDTO.MachineModel;
            machine.InstallDate = machineDTO.InstallDate;
            machine.Status = machineDTO.Status;
        }

        public static MachineDTO ToDto(Machine machine)
        {
            return new MachineDTO
            {
                MachineId = machine.MachineId,
                PostNumber = machine.PostNumber,
                MachineName = machine.MachineName,
                MachineModel = machine.MachineModel,
                InstallDate = machine.InstallDate,
                Status = machine.Status,
            };
        }

    }
}

