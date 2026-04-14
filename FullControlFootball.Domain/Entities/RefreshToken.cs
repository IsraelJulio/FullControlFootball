using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class RefreshToken : AuditableEntity
{
    private RefreshToken() { }

    public RefreshToken(Guid userId, string token, DateTime expiresAtUtc, string? createdByIp)
    {
        UserId = userId;
        Token = token;
        ExpiresAtUtc = expiresAtUtc;
        CreatedByIp = createdByIp;
    }

    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public string? CreatedByIp { get; private set; }
    public string? RevokedByIp { get; private set; }

    public User User { get; private set; } = null!;

    public bool IsExpired(DateTime utcNow) => utcNow >= ExpiresAtUtc;
    public bool IsRevoked => RevokedAtUtc.HasValue;
    public bool IsActive(DateTime utcNow) => !IsRevoked && !IsExpired(utcNow);

    public void Revoke(DateTime revokedAtUtc, string? revokedByIp, string? replacedByToken)
    {
        RevokedAtUtc = revokedAtUtc;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
        MarkUpdatedUtc(revokedAtUtc);
    }
}
