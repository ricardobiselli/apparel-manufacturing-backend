using Application.Models;
using Domain.Models;

namespace Application.Mappers
{
    public static class OperationMapper
    {
        public static OperationDTO ToDto(Operation operation)
        {
            return new OperationDTO
            {
                OperationId = operation.OperationId,
                OperationName = operation.OperationName,
                OperationDescription = operation.OperationDescription,
                BaseTime = operation.BaseTime,
                UnitsPerGarment = operation.UnitsPerGarment,
            };
        }

        public static Operation ToEntity(AddOperationDTO dto)
        {
            return new Operation(
                dto.OperationName,
                dto.OperationDescription,
                dto.BaseTime,
                dto.UnitsPerGarment
            );
        }
    }
}