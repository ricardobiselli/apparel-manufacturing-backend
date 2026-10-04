using Application.Models;
using Domain.Models;
using System;

namespace Application.Mappers
{
    public static class FabricRollMapper
    {
        public static FabricRollDTO ToDto(FabricRoll entity)
        {
            return new FabricRollDTO
            {
                FabricRollId = entity.FabricRollId,
                FabricRollName = entity.FabricRollName,
                Color = entity.Color,
                FabricRollDescription = entity.FabricRollDescription,
                WeightOrLength = entity.WeightOrLength,
                Yield = entity.Yield,
                Date = entity.Date,
                BarCode = entity.BarCode,
                Supplier = entity.Supplier,
                State = entity.State
            };
        }
    }
}