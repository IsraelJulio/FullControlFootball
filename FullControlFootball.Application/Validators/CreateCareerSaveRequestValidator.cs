using FluentValidation;
using FullControlFootball.Application.Features.CareerSaves.Contracts;
using FullControlFootball.Domain.Common;

namespace FullControlFootball.Application.Validators;

public sealed class CreateCareerSaveRequestValidator : AbstractValidator<CreateCareerSaveRequest>
{
    public CreateCareerSaveRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(FieldLengths.Name);
        RuleFor(x => x.GameEdition).NotEmpty().MaximumLength(FieldLengths.GameEdition);
        RuleFor(x => x.CurrentSeasonNumber).GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(FieldLengths.Description);
    }
}
