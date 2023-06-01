using FluentValidation;
using MediatR;
using Micro.WebAPI;
using MyCash.WealthManager.Core.Factories;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Application.Commands.CreateFamily;

public record CreateFamilyRequest(string Name, string Currency, decimal ExpectedMonthyExpenses) : Request<Guid>;

internal sealed class CreateFamilyRequestHandler : IRequestHandler<CreateFamilyRequest, Guid>
{
    private readonly IFamilyFactory _familyFactory;
    private readonly IFamilyRepository _familyRepository;

    public CreateFamilyRequestHandler(IFamilyFactory familyFactory, IFamilyRepository familyRepository)
    {
        _familyFactory = familyFactory;
        _familyRepository = familyRepository;
    }

    public async Task<Guid> Handle(CreateFamilyRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateFamilyValidator();
        validator.ValidateAndThrow(request);

        var familySettings = new FamilySettings
        {
            Currency = request.Currency,
            ExpectedMonthyExpenses = request.ExpectedMonthyExpenses
        };
        var family = await _familyFactory.CreateAsync(request.UserId, request.Name, familySettings, cancellationToken);
        await _familyRepository.CreateFamilyAsync(family, cancellationToken);

        return family.Id;
    }
}
