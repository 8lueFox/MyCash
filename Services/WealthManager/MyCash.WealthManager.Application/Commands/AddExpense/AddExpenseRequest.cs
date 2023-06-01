using FluentValidation;
using MediatR;
using Micro.WebAPI;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Application.Commands.AddExpense;

public record AddExpenseRequest(
    Guid FamilyId,
    string Name, 
    string? Description, 
    decimal Count, 
    string Currency, 
    Date SendDate,
    bool IsActive, 
    string ExpenseType, 
    int? Period) : Request<Guid>;

public class AddExpenseRequestHandler : IRequestHandler<AddExpenseRequest, Guid>
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IFamilyService _familyService;

    public AddExpenseRequestHandler(IFamilyRepository familyRepository, IFamilyService familyService)
    {
        _familyRepository = familyRepository;
        _familyService = familyService;
    }

    public async Task<Guid> Handle(AddExpenseRequest request, CancellationToken cancellationToken)
    {
        var validator = new AddExpenseValidator(_familyRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var family = await _familyRepository.GetFamilyAsync(request.FamilyId, cancellationToken);

        var period = request.Period is null ? 0 : (int)request.Period;
        var expense = _familyService.AddExpense(family, request.Name, new Value(request.Count, request.Currency ), request.SendDate, request.IsActive, request.ExpenseType, new Period(period), request.Description); ;

        await _familyRepository.UpdateFamilyAsync(family, cancellationToken);

        return expense.Id;
    }
}
