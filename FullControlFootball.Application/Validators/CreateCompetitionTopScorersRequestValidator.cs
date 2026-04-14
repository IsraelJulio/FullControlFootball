using FluentValidation;
using FullControlFootball.Application.Features.Rankings.Contracts;
using FullControlFootball.Domain.Common;

namespace FullControlFootball.Application.Validators;

public sealed class CreateCompetitionTopScorersRequestValidator : AbstractValidator<CreateCompetitionTopScorersRequest>
{
    public CreateCompetitionTopScorersRequestValidator()
    {
        RuleFor(x => x.SeasonCompetitionId).NotEmpty();
        RuleFor(x => x.Rows).NotEmpty();

        RuleForEach(x => x.Rows).ChildRules(row =>
        {
            row.RuleFor(r => r.PlayerNameSnapshot).NotEmpty().MaximumLength(FieldLengths.SnapshotName);
            row.RuleFor(r => r.ClubNameSnapshot).MaximumLength(FieldLengths.SnapshotName);
            row.RuleFor(r => r.Position).GreaterThan(0);
            row.RuleFor(r => r.Goals).GreaterThanOrEqualTo(0);
        });
    }
}
