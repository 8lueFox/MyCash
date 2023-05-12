using FluentValidation;
using MediatR;
using MyCash.WealthManager.Core.Factories;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Commands.CreateFamily;

public record CreateFamilyRequest(Guid UserId, string Name) : IRequest<Guid>;

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

        var family = await _familyFactory.CreateAsync(request.UserId, request.Name, cancellationToken);
        await _familyRepository.CreateFamilyAsync(family, cancellationToken);

        return family.Id;
    }
}
