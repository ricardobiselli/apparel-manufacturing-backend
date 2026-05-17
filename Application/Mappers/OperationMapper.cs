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
                BaseTime = operation.BaseTime
            };
        }
    }
}