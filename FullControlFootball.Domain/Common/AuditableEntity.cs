namespace FullControlFootball.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime? UpdatedAtUtc { get; protected set; }

    public void MarkUpdatedUtc(DateTime utcNow)
    {
        UpdatedAtUtc = utcNow;
    }
}
