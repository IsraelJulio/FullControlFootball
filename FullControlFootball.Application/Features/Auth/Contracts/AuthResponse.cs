namespace FullControlFootball.Application.Features.Auth.Contracts;

public sealed record AuthResponse(Guid UserId, string Name, string Email, string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAtUtc);
