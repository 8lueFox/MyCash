using FluentValidation;
using MyCash.WealthManager.Application.Commands.CreateFamily;
using MyCash.WealthManager.Core.Exceptions;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.AddIncome;
internal class AddIncomeValidator : AbstractValidator<AddIncomeRequest>
{
    private readonly IFamilyRepository _familyRepository;

    public AddIncomeValidator(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;

        RuleFor(x => x)
            .MustAsync(CheckUserHasPrivileges)
                .WithMessage("Użytkownik nie ma uprawnień do tej rodziny.");
        RuleFor(x => x.FamilyId).CustomAsync(CheckFamilyExists);
        RuleFor(x => x.Name.Length)
            .InclusiveBetween(3, 100)
                .WithMessage("Długość nazwy przychodu powinna być pomiędzy 3 a 100 znaków.");
        RuleFor(x => x.Currency)
            .NotNull()
                .WithMessage("Skrót waluty nie może być pusty!")
            .MaximumLength(5)
                .WithMessage("Skrót waluty nie może mieć więcej znaków niż 5.");
        RuleFor(x => x.CountGross)
            .NotNull()
                .WithMessage("Przychód nie może być pusty.")
            .GreaterThan(0)
                .WithMessage("Przychó nie może być ujemny.")
            .PrecisionScale(int.MaxValue, 2, true);
        RuleFor(x => x.CountNet)
            .NotNull()
                .WithMessage("Przychód nie może być pusty.")
            .GreaterThan(0)
                .WithMessage("Przychó nie może być ujemny.")
            .PrecisionScale(int.MaxValue, 2, true);
    }

    private async Task CheckFamilyExists(Guid familyId,
        IValidationContext context,
        CancellationToken cancellationToken)
    {
        var _ = await _familyRepository.GetFamilyAsync(familyId, cancellationToken)
            ?? throw new NotFoundException($"Podany identyfikator rodziny ({familyId}) nie istnieje.");
    }

    private async Task<bool> CheckUserHasPrivileges(AddIncomeRequest addIncomeRequest, CancellationToken cancellationToken)
    {
        var family = await _familyRepository.GetFamilyAsync(addIncomeRequest.FamilyId, cancellationToken);

        if (family is not null && family.UserId.Value == addIncomeRequest.UserId)
            return true;

        return false;
    }
}