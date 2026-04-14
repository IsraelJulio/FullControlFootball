using FullControlFootball.Application.Features.Auth.Contracts;

namespace FullControlFootball.Application.Abstractions.Authentication;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, string? ipAddress, CancellationToken cancellationToken);
    Task<AuthResponse> LoginAsync(LoginRequest request, string? ipAddress, CancellationToken cancellationToken);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress, CancellationToken cancellationToken);
    Task<AuthResponse> LoginWithGoogleAsync(GoogleLoginRequest request, string? ipAddress, CancellationToken cancellationToken);
}
