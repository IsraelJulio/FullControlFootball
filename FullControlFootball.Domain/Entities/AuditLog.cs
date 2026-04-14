using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? CareerSaveId { get; set; }
    public string EntityName { get; set; } = null!;
    public Guid? EntityId { get; set; }
    public string ActionType { get; set; } = null!;
    public string? OldValuesJson { get; set; }
    public string? NewValuesJson { get; set; }

    public User? User { get; set; }
    public CareerSave? CareerSave { get; set; }
}