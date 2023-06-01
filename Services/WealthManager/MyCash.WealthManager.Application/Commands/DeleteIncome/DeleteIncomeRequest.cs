using FluentValidation;
using MediatR;
using Micro.WebAPI;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.DeleteIncome;

public record DeleteIncomeRequest(Guid FamilyId, Guid IncomeId) : Request;

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

        _familyService.DeleteIncome(family, request.IncomeId);

        await _familyRepository.UpdateFamilyAsync(family, cancellationToken);
    }
}
