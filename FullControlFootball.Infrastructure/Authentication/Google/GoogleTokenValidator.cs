using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace FullControlFootball.Infrastructure.Authentication.Google;

public sealed class GoogleTokenValidator : IGoogleTokenValidator
{
    private static readonly string[] ValidIssuers =
    {
        "accounts.google.com",
        "https://accounts.google.com"
    };

    private readonly GoogleAuthSettings _settings;

    public GoogleTokenValidator(IOptions<GoogleAuthSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<GoogleUserPayload> ValidateAsync(string idToken, CancellationToken cancellationToken)
    {
        _ = cancellationToken;

        if (string.IsNullOrWhiteSpace(_settings.ClientId))
        {
            throw new InvalidOperationException("Google login is not configured. GoogleAuth:ClientId is missing.");
        }

        if (string.IsNullOrWhiteSpace(idToken))
        {
            throw new UnauthorizedAccessException("Google ID token is required.");
        }

        var payload = await GoogleJsonWebSignature.ValidateAsync(
            idToken,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _settings.ClientId }
            });

        if (!ValidIssuers.Contains(payload.Issuer, StringComparer.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException("Google token issuer is invalid.");
        }

        if (payload.ExpirationTimeSeconds is null)
        {
            throw new UnauthorizedAccessException("Google token expiration is invalid.");
        }

        var expirationUtc = DateTimeOffset.FromUnixTimeSeconds(payload.ExpirationTimeSeconds.Value).UtcDateTime;
        if (expirationUtc <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Google token has expired.");
        }

        if (string.IsNullOrWhiteSpace(payload.Subject) || string.IsNullOrWhiteSpace(payload.Email))
        {
            throw new UnauthorizedAccessException("Google token payload is incomplete.");
        }

        return new GoogleUserPayload(
            payload.Subject,
            payload.Email.Trim().ToLowerInvariant(),
            string.IsNullOrWhiteSpace(payload.Name) ? payload.Email : payload.Name,
            payload.Picture);
    }
}
