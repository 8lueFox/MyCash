using FluentValidation;
using MyCash.WealthManager.Application.Commands.DeleteExpense;
using MyCash.WealthManager.Core.Exceptions;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.DeleteIncome;

internal class DeleteIncomeValidator : AbstractValidator<DeleteIncomeRequest>
{
    private IFamilyRepository _familyRepository;

    public DeleteIncomeValidator(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;

        RuleFor(x => x)
            .MustAsync(CheckUserHasPrivileges)
                .WithMessage("Użytkownik nie ma uprawnień do tej rodziny.");
        RuleFor(x => x.FamilyId).CustomAsync(CheckFamilyExists);
    }

    private async Task CheckFamilyExists(Guid familyId,
        IValidationContext context,
        CancellationToken cancellationToken)
    {
        var _ = await _familyRepository.GetFamilyAsync(familyId, cancellationToken)
            ?? throw new NotFoundException($"Podany identyfikator rodziny ({familyId}) nie istnieje.");
    }

    private async Task<bool> CheckUserHasPrivileges(DeleteIncomeRequest deleteIncomeRequest, CancellationToken cancellationToken)
    {
        var family = await _familyRepository.GetFamilyAsync(deleteIncomeRequest.FamilyId, cancellationToken);

        if (family is not null && family.UserId.Value == deleteIncomeRequest.UserId)
            return true;

        return false;
    }
}