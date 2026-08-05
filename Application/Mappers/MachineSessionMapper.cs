using Application.Models;
using Application.Models.Requests;
using Domain.Models;

namespace Application.Mappers
{
    public class MachineSessionMapper
    {

        //public static MachineSession ToEntity(AddMachineSessionDTO machineSessionDTO)
        //{
        //    return new MachineSession(machineSessionDTO.OrderId, machineSessionDTO.MachineId, machineSessionDTO.GarmentId,
        //        machineSessionDTO.OperationId, machineSessionDTO.Status);
        //}

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
                OperationName = machineSession.OperationName,
                OperationDescription = machineSession.OperationDescription,
                BaseTime = machineSession.BaseTime,
                UnitsPerGarment = machineSession.UnitsPerGarment,
                CreatedAt = machineSession.CreatedAt,
                StartedAt = machineSession.StartedAt,
                EndedAt = machineSession.EndedAt,
                Status = machineSession.Status,
            };
        }

    }
}

//   public int OperationId { get; set; }
//public string OperationName { get; set; }
//public string OperationDescription { get; set; }
//public double BaseTime { get; set; }
//public int UnitsPerGarment { get; set; }