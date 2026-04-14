using FluentValidation;
using FullControlFootball.Application.Features.Seasons.Contracts;
using FullControlFootball.Domain.Common;

namespace FullControlFootball.Application.Validators;

public sealed class CreateSeasonRequestValidator : AbstractValidator<CreateSeasonRequest>
{
    public CreateSeasonRequestValidator()
    {
        RuleFor(x => x.CareerSaveId).NotEmpty();
        RuleFor(x => x.Number).GreaterThan(0);
        RuleFor(x => x.Label).NotEmpty().MaximumLength(FieldLengths.Label);
        RuleFor(x => x)
            .Must(x => !x.StartedAt.HasValue || !x.EndedAt.HasValue || x.StartedAt <= x.EndedAt)
            .WithMessage("StartedAt must be less than or equal to EndedAt.");
    }
}
