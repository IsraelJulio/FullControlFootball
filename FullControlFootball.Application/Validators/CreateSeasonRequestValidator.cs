using FluentValidation;
using FullControlFootball.Application.Features.Seasons.Contracts;

namespace FullControlFootball.Application.Validators;

public sealed class CreateSeasonRequestValidator : AbstractValidator<CreateSeasonRequest>
{
    public CreateSeasonRequestValidator()
    {
        RuleFor(x => x.CareerSaveId).NotEmpty();
        RuleFor(x => x.Number).GreaterThan(0);
        RuleFor(x => x.Label).NotEmpty().MaximumLength(100);
        RuleFor(x => x)
            .Must(x => !x.StartedAt.HasValue || !x.EndedAt.HasValue || x.StartedAt <= x.EndedAt)
            .WithMessage("StartedAt must be less than or equal to EndedAt.");
    }
}
