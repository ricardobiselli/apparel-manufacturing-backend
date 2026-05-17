using Application.Models;
using Application.Models.Requests;
using Domain.Models;

namespace Application.Mappers
{
    public static class OperationLogMapper
    {
        public static OperationLog ToEntity(AddOperationLogDTO dto)
        {
            return new OperationLog
            {
                MachineSessionId = dto.MachineSessionId,
            };
        }

        public static OperationLogDTO ToDto(OperationLog entity)
        {
            return new OperationLogDTO
            {
                MachineSessionId = entity.MachineSessionId,
                Timestamp = entity.Timestamp,
            };
        }
    }
}
