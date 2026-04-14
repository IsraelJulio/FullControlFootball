using FluentValidation;
using FullControlFootball.Application.Features.Transfers.Contracts;
using FullControlFootball.Domain.Common;

namespace FullControlFootball.Application.Validators;

public sealed class CreateTransferTransactionRequestValidator : AbstractValidator<CreateTransferTransactionRequest>
{
    public CreateTransferTransactionRequestValidator()
    {
        RuleFor(x => x.CareerSaveId).NotEmpty();
        RuleFor(x => x.SeasonId).NotEmpty();
        RuleFor(x => x.PlayerNameSnapshot).NotEmpty().MaximumLength(FieldLengths.SnapshotName);
        RuleFor(x => x.FromClubNameSnapshot).MaximumLength(FieldLengths.SnapshotName);
        RuleFor(x => x.ToClubNameSnapshot).MaximumLength(FieldLengths.SnapshotName);
        RuleFor(x => x.Currency).MaximumLength(FieldLengths.Currency);
        RuleFor(x => x.Notes).MaximumLength(FieldLengths.Notes);
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).When(x => x.Amount.HasValue);
    }
}
