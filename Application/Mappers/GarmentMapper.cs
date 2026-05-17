using Application.Models;
using Domain.Models;

namespace Application.Mappers
{
    public static class GarmentMapper
    {
        public static GarmentDTO ToDto(Garment garment)
        {
            return new GarmentDTO
            {
                GarmentId = garment.GarmentId,
                GarmentName = garment.GarmentName,
                GarmentDescription = garment.GarmentDescription,
                SAM = garment.SAM,

                Operations = garment.Operations != null
                    ? garment.Operations.Select(OperationMapper.ToDto).ToList()
                    : new List<OperationDTO>()
            };
        }
    }
}