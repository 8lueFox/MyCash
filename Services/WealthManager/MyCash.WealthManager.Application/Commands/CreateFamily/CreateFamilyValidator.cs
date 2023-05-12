using FluentValidation;

namespace MyCash.WealthManager.Application.Commands.CreateFamily;

internal class CreateFamilyValidator : AbstractValidator<CreateFamilyRequest>
{
    public CreateFamilyValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
                .WithMessage("Nazwa rodziny nie może być pusta.")
            .MinimumLength(5)
                .WithMessage("Nazwa rodziny musi mieć przynajmniej 5 znaków.")
            .MaximumLength(100)
                .WithMessage("Nazwa rodziny może mieć maksymalnie 100 znaków.");
    }
}
