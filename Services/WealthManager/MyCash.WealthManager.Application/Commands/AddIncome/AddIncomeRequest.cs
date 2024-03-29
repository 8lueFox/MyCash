﻿using FluentValidation;
using MediatR;
using Micro.WebAPI;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Application.Commands.AddIncome;

public record AddIncomeRequest(
    Guid FamilyId,
    string Name,
    string? Description,
    decimal CountNet,
    decimal CountGross,
    string Currency,
    Date ReceiveDate,
    bool IsActive,
    string IncomeType,
    int Period) : Request<Guid>;

public class AddIncomeRequestHandler : IRequestHandler<AddIncomeRequest, Guid>
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IFamilyService _familyService;

    public AddIncomeRequestHandler(IFamilyRepository familyRepository, IFamilyService familyService)
    {
        _familyRepository = familyRepository;
        _familyService = familyService;
    }

    public async Task<Guid> Handle(AddIncomeRequest request, CancellationToken cancellationToken)
    {
        var validator = new AddIncomeValidator(_familyRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var family = await _familyRepository.GetFamilyAsync(request.FamilyId, cancellationToken);

        Income? income = null;
        try
        {
            income = _familyService.AddIncome(family, request.Name, new Value(request.CountNet, request.Currency), new Value(request.CountGross, request.Currency), request.ReceiveDate, request.IsActive, request.IncomeType, new Period(request.Period), request.Description);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }

        await _familyRepository.UpdateFamilyAsync(family, cancellationToken);

        return income != null ? income.Id : Guid.Empty;
    }
}
