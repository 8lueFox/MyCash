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
    private readonly IUserRepository _userRepository;

    public CreateFamilyRequestHandler(IFamilyFactory familyFactory, IFamilyRepository familyRepository, IUserRepository userRepository)
    {
        _familyFactory = familyFactory;
        _familyRepository = familyRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateFamilyRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateFamilyValidator();
        validator.ValidateAndThrow(request);

        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
        var familySettings = new FamilySettings
        {
            Currency = request.Currency,
            ExpectedMonthyExpenses = request.ExpectedMonthyExpenses
        };
        var family = await _familyFactory.CreateAsync(user.Id, request.Name, familySettings, cancellationToken);
        await _familyRepository.CreateFamilyAsync(family, cancellationToken);

        return family.Id;
    }
}
