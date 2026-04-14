using FluentValidation;
using FullControlFootball.Application.Features.Auth.Contracts;

namespace FullControlFootball.Application.Features.Auth.Validators;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
