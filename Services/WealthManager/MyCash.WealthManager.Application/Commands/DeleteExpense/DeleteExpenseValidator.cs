using FluentValidation;
using MyCash.WealthManager.Core.Exceptions;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.DeleteExpense;

internal class DeleteExpenseValidator : AbstractValidator<DeleteExpenseRequest>
{
    private readonly IFamilyRepository _familyRepository;

    public DeleteExpenseValidator(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;

        RuleFor(x => x.FamilyId).CustomAsync(CheckFamilyExists);
    }

    private async Task CheckFamilyExists(Guid familyId,
        IValidationContext context,
        CancellationToken cancellationToken)
    {
        var _ = await _familyRepository.GetFamilyAsync(familyId, cancellationToken) 
            ?? throw new NotFoundException($"Podany identyfikator rodziny ({familyId}) nie istnieje.");
    }
}
