using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public sealed class UserAuthProvider : AuditableEntity
{
    private UserAuthProvider() { }

    public UserAuthProvider(Guid userId, AuthProviderType provider, string providerUserId, string? providerEmail)
    {
        UserId = userId;
        Provider = provider;
        ProviderUserId = providerUserId;
        ProviderEmail = providerEmail;
    }

    public Guid UserId { get; private set; }
    public AuthProviderType Provider { get; private set; }
    public string ProviderUserId { get; private set; } = null!;
    public string? ProviderEmail { get; private set; }

    public User User { get; private set; } = null!;
}
