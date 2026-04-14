using FullControlFootball.Domain.Entities;

namespace FullControlFootball.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAtUtc) GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
