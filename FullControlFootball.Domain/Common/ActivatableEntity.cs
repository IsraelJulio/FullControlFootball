namespace FullControlFootball.Domain.Common;

public abstract class ActivatableEntity : AuditableEntity
{
    public bool IsActive { get; protected set; } = true;

    public void Activate(DateTime utcNow)
    {
        IsActive = true;
        MarkUpdatedUtc(utcNow);
    }

    public void Deactivate(DateTime utcNow)
    {
        IsActive = false;
        MarkUpdatedUtc(utcNow);
    }
}
