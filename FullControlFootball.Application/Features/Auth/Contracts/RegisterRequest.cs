namespace FullControlFootball.Application.Features.Auth.Contracts;

public sealed record RegisterRequest(string Name, string Email, string Password);
