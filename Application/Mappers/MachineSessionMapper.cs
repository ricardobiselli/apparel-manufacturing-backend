using Application.Models;
using Application.Models.Requests;
using Domain.Models;

namespace Application.Mappers
{
    public class MachineSessionMapper
    {

        public static MachineSession ToEntity(AddMachineSessionDTO machineSessionDTO)
        {
            return new MachineSession(machineSessionDTO.OrderId, machineSessionDTO.MachineId, machineSessionDTO.GarmentId,
                machineSessionDTO.OperationId, machineSessionDTO.Status);
        }

        public static MachineSessionDTO ToDto(MachineSession machineSession)
        {
            return new MachineSessionDTO
            {
                OrderId = machineSession.OrderId,
                MachineSessionId = machineSession.MachineSessionId,
                MachineId = machineSession.MachineId,

                GarmentId = machineSession.GarmentId,
                GarmentName = machineSession.Garment.GarmentName,

                OperationId = machineSession.OperationId,
                OperationName = machineSession.Operation.OperationName,

                StartedAt = machineSession.StartedAt,
                Status = machineSession.Status,
            };
        }

    }
}

