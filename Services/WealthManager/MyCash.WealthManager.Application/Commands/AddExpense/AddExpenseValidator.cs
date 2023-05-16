using FluentValidation;
using MyCash.WealthManager.Application.Commands.DeleteExpense;
using MyCash.WealthManager.Core.Exceptions;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.AddExpense;

internal class AddExpenseValidator : AbstractValidator<AddExpenseRequest>
{
    private readonly IFamilyRepository _familyRepository;

    public AddExpenseValidator(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;

        RuleFor(x => x)
            .MustAsync(CheckUserHasPrivileges)
                .WithMessage("Użytkownik nie ma uprawnień do tej rodziny.");
        RuleFor(x => x.FamilyId).CustomAsync(CheckFamilyExists);
        RuleFor(x => x.Name.Length)
            .InclusiveBetween(3, 100)
                .WithMessage("Długość nazwy wydatku powinna być pomiędzy 3 a 100 znaków.");
        RuleFor(x => x.Currency)
            .NotNull()
                .WithMessage("Skrót waluty nie może być pusty!")
            .MaximumLength(5)
                .WithMessage("Skrót waluty nie może mieć więcej znaków niż 5.");
        RuleFor(x => x.Count)
            .NotNull()
                .WithMessage("Wydatek nie może być pusty.")
            .GreaterThan(0)
                .WithMessage("Wydatek nie może być ujemny.")
            .PrecisionScale(int.MaxValue, 2, true);
    }

    private async Task CheckFamilyExists(Guid familyId,
        IValidationContext context,
        CancellationToken cancellationToken)
    {
        var _ = await _familyRepository.GetFamilyAsync(familyId, cancellationToken)
            ?? throw new NotFoundException($"Podany identyfikator rodziny ({familyId}) nie istnieje.");
    }

    private async Task<bool> CheckUserHasPrivileges(AddExpenseRequest addExpenseRequest, CancellationToken cancellationToken)
    {
        var family = await _familyRepository.GetFamilyAsync(addExpenseRequest.FamilyId, cancellationToken);

        if (family is not null && family.UserId.Value == addExpenseRequest.UserId)
            return true;

        return false;
    }
}
