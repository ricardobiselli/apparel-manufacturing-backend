using Domain.Enums;

namespace Domain.Models;

public class FabricRoll
{
    public int FabricRollId { get; set; }
    public string FabricRollName { get; set; }
    public string Color { get; set; }
    public string? FabricRollDescription { get; set; }
    public double WeightOrLength { get; set; }
    public double Yield { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int? BarCode { get; set; }
    public string Supplier { get; set; }
    public FabricRollState State { get; set; } = FabricRollState.Available;
    public FabricRoll() { }
}
