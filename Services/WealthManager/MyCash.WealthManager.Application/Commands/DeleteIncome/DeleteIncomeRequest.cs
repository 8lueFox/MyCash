using FluentValidation;
using MediatR;
using MyCash.WealthManager.Application.Commands.DeleteExpense;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.DeleteIncome;

public record DeleteIncomeRequest(Guid FamilyId, Guid IncomeId) : IRequest;

public sealed class DeleteIncomeRequestHandler : IRequestHandler<DeleteIncomeRequest>
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IFamilyService _familyService;

    public DeleteIncomeRequestHandler(IFamilyRepository familyRepository, IFamilyService familyService)
    {
        _familyRepository = familyRepository;
        _familyService = familyService;
    }

    public async Task Handle(DeleteIncomeRequest request, CancellationToken cancellationToken)
    {
        var validator = new DeleteIncomeValidator(_familyRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var family = await _familyRepository.GetFamilyAsync(request.FamilyId, cancellationToken);

        await _familyService.DeleteIncome(family, request.IncomeId);

        await _familyRepository.UpdateFamilyAsync(family, cancellationToken);
    }
}
