using FullControlFootball.Application.Abstractions.Authentication;

namespace FullControlFootball.Infrastructure.Authentication.Passwords;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
