namespace FullControlFootball.Infrastructure.Authentication.Google;

public interface IGoogleTokenValidator
{
    Task<GoogleUserPayload> ValidateAsync(string idToken, CancellationToken cancellationToken);
}

public sealed record GoogleUserPayload(
    string ProviderUserId,
    string Email,
    string Name,
    string? PictureUrl);
