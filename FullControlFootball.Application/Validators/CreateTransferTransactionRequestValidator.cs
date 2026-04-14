using FluentValidation;
using FullControlFootball.Application.Features.Transfers.Contracts;

namespace FullControlFootball.Application.Validators;

public sealed class CreateTransferTransactionRequestValidator : AbstractValidator<CreateTransferTransactionRequest>
{
    public CreateTransferTransactionRequestValidator()
    {
        RuleFor(x => x.CareerSaveId).NotEmpty();
        RuleFor(x => x.SeasonId).NotEmpty();
        RuleFor(x => x.PlayerNameSnapshot).NotEmpty().MaximumLength(180);
        RuleFor(x => x.FromClubNameSnapshot).MaximumLength(180);
        RuleFor(x => x.ToClubNameSnapshot).MaximumLength(180);
        RuleFor(x => x.Currency).MaximumLength(10);
        RuleFor(x => x.Notes).MaximumLength(2000);
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).When(x => x.Amount.HasValue);
    }
}
