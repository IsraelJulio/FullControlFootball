using FluentValidation;
using FullControlFootball.Application.Features.CompetitionStandings.Contracts;
using FullControlFootball.Domain.Common;

namespace FullControlFootball.Application.Validators;

public sealed class CreateCompetitionStandingRequestValidator : AbstractValidator<CreateCompetitionStandingRequest>
{
    public CreateCompetitionStandingRequestValidator()
    {
        RuleFor(x => x.SeasonCompetitionId).NotEmpty();
        RuleFor(x => x.Rows).NotEmpty();
        RuleFor(x => x.SnapshotDateUtc)
            .Must(x => x.Kind == DateTimeKind.Utc || x.Kind == DateTimeKind.Unspecified)
            .WithMessage("SnapshotDateUtc must be handled as UTC.");

        RuleForEach(x => x.Rows).ChildRules(row =>
        {
            row.RuleFor(r => r.ClubNameSnapshot).NotEmpty().MaximumLength(FieldLengths.SnapshotName);
            row.RuleFor(r => r.Position).GreaterThan(0);
            row.RuleFor(r => r.Played).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.Wins).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.Draws).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.Losses).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.GoalsFor).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.GoalsAgainst).GreaterThanOrEqualTo(0);
            row.RuleFor(r => r.Points).GreaterThanOrEqualTo(0);
        });
    }
}
