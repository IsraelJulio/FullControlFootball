using FullControlFootball.Application.Abstractions.Authentication;
using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Abstractions.Time;
using FullControlFootball.Application.Features.Auth.Contracts;
using FullControlFootball.Domain.Entities;
using FullControlFootball.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FullControlFootball.Infrastructure.Authentication.Google;
using FullControlFootball.Infrastructure.Authentication.Jwt;

namespace FullControlFootball.Infrastructure.Authentication.Services;

public sealed class AuthService : IAuthService
{
    private readonly IAppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGoogleTokenValidator _googleTokenValidator;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IAppDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider,
        IGoogleTokenValidator googleTokenValidator,
        IOptions<JwtSettings> jwtOptions)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
        _googleTokenValidator = googleTokenValidator;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, string? ipAddress, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var exists = await _dbContext.Users.AnyAsync(x => x.Email == normalizedEmail, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("An account with this email already exists.");
        }

        var user = new User(request.Name.Trim(), normalizedEmail, _passwordHasher.Hash(request.Password), null, null);

        _dbContext.Users.Add(user);

        var refreshToken = CreateRefreshToken(user.Id, ipAddress);
        _dbContext.RefreshTokens.Add(refreshToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAtUtc) = _jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthResponse(user.Id, user.Name, user.Email, accessToken, refreshToken.Token, expiresAtUtc);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, string? ipAddress, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == normalizedEmail, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (string.IsNullOrWhiteSpace(user.PasswordHash) || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        user.MarkLogin(_dateTimeProvider.UtcNow);

        var refreshToken = CreateRefreshToken(user.Id, ipAddress);
        _dbContext.RefreshTokens.Add(refreshToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAtUtc) = _jwtTokenGenerator.GenerateAccessToken(user);
        return new AuthResponse(user.Id, user.Name, user.Email, accessToken, refreshToken.Token, expiresAtUtc);
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress, CancellationToken cancellationToken)
    {
        var existingToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!existingToken.IsActive(_dateTimeProvider.UtcNow))
        {
            throw new UnauthorizedAccessException("Refresh token is no longer active.");
        }

        var replacement = CreateRefreshToken(existingToken.UserId, ipAddress);
        existingToken.Revoke(_dateTimeProvider.UtcNow, ipAddress, replacement.Token);

        _dbContext.RefreshTokens.Add(replacement);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAtUtc) = _jwtTokenGenerator.GenerateAccessToken(existingToken.User);
        return new AuthResponse(existingToken.User.Id, existingToken.User.Name, existingToken.User.Email, accessToken, replacement.Token, expiresAtUtc);
    }

    public async Task<AuthResponse> LoginWithGoogleAsync(GoogleLoginRequest request, string? ipAddress, CancellationToken cancellationToken)
    {
        var payload = await _googleTokenValidator.ValidateAsync(request.IdToken, cancellationToken);

        var user = await _dbContext.Users
            .Include(x => x.AuthProviders)
            .SingleOrDefaultAsync(x => x.Email == payload.Email, cancellationToken);

        if (user is null)
        {
            user = new User(payload.Name, payload.Email, null, payload.PictureUrl, null);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var hasGoogleProvider = user.AuthProviders.Any(x =>
            x.Provider == AuthProviderType.Google &&
            x.ProviderUserId == payload.ProviderUserId);

        if (!hasGoogleProvider)
        {
            var provider = new UserAuthProvider(
                user.Id,
                AuthProviderType.Google,
                payload.ProviderUserId,
                payload.Email);

            _dbContext.UserAuthProviders.Add(provider);
        }

        user.MarkLogin(_dateTimeProvider.UtcNow);

        var refreshToken = CreateRefreshToken(user.Id, ipAddress);
        _dbContext.RefreshTokens.Add(refreshToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAtUtc) = _jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthResponse(
            user.Id,
            user.Name,
            user.Email,
            accessToken,
            refreshToken.Token,
            expiresAtUtc);
    }

    private RefreshToken CreateRefreshToken(Guid userId, string? ipAddress)
    {
        return new RefreshToken(
            userId,
            _jwtTokenGenerator.GenerateRefreshToken(),
            _dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            ipAddress);
    }
}
