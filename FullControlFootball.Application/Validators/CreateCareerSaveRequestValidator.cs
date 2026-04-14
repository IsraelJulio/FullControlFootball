using FluentValidation;
using FullControlFootball.Application.Features.CareerSaves.Contracts;

namespace FullControlFootball.Application.Validators;

public sealed class CreateCareerSaveRequestValidator : AbstractValidator<CreateCareerSaveRequest>
{
    public CreateCareerSaveRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.GameEdition).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CurrentSeasonNumber).GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(2000);
    }
}
