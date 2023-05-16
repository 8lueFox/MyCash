﻿using FluentValidation;
using MediatR;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.DeleteExpense;

public record DeleteExpenseRequest(Guid UserId, Guid FamilyId, Guid ExpenseId) : IRequest;

internal sealed class DeleteExpenseRequestHandler : IRequestHandler<DeleteExpenseRequest>
{
    private readonly IFamilyService _familyService;
    private readonly IFamilyRepository _familyRepository;

    public DeleteExpenseRequestHandler(IFamilyService familyService, IFamilyRepository familyRepository)
    {
        _familyService = familyService;
        _familyRepository = familyRepository;
    }

    public async Task Handle(DeleteExpenseRequest request, CancellationToken cancellationToken)
    {
        var validator = new DeleteExpenseValidator(_familyRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var family = await _familyRepository.GetFamilyAsync(request.FamilyId, cancellationToken);

        _familyService.DeleteExpense(family, request.ExpenseId);

        await _familyRepository.UpdateFamilyAsync(family, cancellationToken);
    }
}