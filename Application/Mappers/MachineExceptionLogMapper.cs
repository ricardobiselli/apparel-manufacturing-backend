using Application.Models;
using Application.Models.Requests;

namespace Application.Mappers
{
    public static class MachineExceptionLogMapper
    {
        public static MachineExceptionLog ToEntity(AddMachineExceptionLogDTO machineExceptionLogDTO)
        {
            return new MachineExceptionLog
            {
                MachineSessionId = machineExceptionLogDTO.MachineSessionId,
                Type = machineExceptionLogDTO.Type,

            };
        }

        public static MachineExceptionLogDTO ToDto(MachineExceptionLog machineExceptionLog)
        {
            return new MachineExceptionLogDTO
            {
                MachineEventId = machineExceptionLog.MachineEventId,
                MachineSessionId = machineExceptionLog.MachineSessionId,
                Type = machineExceptionLog.Type,
                TimeStamp = machineExceptionLog.Timestamp,
            };
        }
    }
}
