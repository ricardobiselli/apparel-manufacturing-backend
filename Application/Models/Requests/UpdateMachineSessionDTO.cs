using Domain.Enums;

namespace Application.Models.Requests;

public class UpdateMachineSessionDTO
{
    // New status for the session (optional - allow partial updates)
    public MachineSessionStatus? Status { get; set; }

    // Optional explicit timestamps; service will set sensible defaults when missing.
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    // Optional user id. Controller will populate this with the authenticated operator id
    // when an operator starts the session (so clients cannot impersonate other users).
    public int? UserId { get; set; }

    // update Snapshot
    public string? OperationName { get; set; }
    public string? OperationDescription { get; set; }
    public double? BaseTime { get; set; }
    public int? UnitsPerGarment { get; set; }
}