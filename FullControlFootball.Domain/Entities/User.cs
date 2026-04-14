using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class User : ActivatableEntity
{
    private readonly List<UserAuthProvider> _authProviders = [];
    private readonly List<RefreshToken> _refreshTokens = [];
    private readonly List<CareerSave> _careerSaves = [];

    private User() { }

    public User(string name, string email, string? passwordHash, string? profileImageUrl, string? preferredTheme)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        ProfileImageUrl = profileImageUrl;
        PreferredTheme = preferredTheme;
    }

    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? PasswordHash { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public string? PreferredTheme { get; private set; }
    public DateTime? LastLoginAtUtc { get; private set; }

    public IReadOnlyCollection<UserAuthProvider> AuthProviders => _authProviders;
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
    public IReadOnlyCollection<CareerSave> CareerSaves => _careerSaves;

    public void UpdateProfile(string name, string? profileImageUrl, string? preferredTheme, DateTime utcNow)
    {
        Name = name;
        ProfileImageUrl = profileImageUrl;
        PreferredTheme = preferredTheme;
        MarkUpdatedUtc(utcNow);
    }

    public void UpdatePasswordHash(string passwordHash, DateTime utcNow)
    {
        PasswordHash = passwordHash;
        MarkUpdatedUtc(utcNow);
    }

    public void MarkLogin(DateTime utcNow)
    {
        LastLoginAtUtc = utcNow;
        MarkUpdatedUtc(utcNow);
    }
}
