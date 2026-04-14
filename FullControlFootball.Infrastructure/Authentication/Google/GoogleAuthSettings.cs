namespace FullControlFootball.Infrastructure.Authentication.Google;

public sealed class GoogleAuthSettings
{
    public const string SectionName = "GoogleAuth";

    public string ClientId { get; init; } = null!;
}
