using Domain.Enums;
using Domain.Models;

public class Bundle
{
    public int BundleId { get; set; }

    public int CutBatchId { get; set; }
    public CutBatch CutBatch { get; set; }

    public int Quantity { get; set; }

    public BundleStatus Status { get; set; }

    public ICollection<BundleSize> Sizes { get; set; }
        = new List<BundleSize>();

    public ICollection<MachineSession> MachineSessions { get; set; }
        = new List<MachineSession>();

    public DateTime CreatedAt { get; set; }
}