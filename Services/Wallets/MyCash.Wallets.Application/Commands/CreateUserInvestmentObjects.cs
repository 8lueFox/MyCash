using MediatR;
using MyCash.Wallets.Core.Factories;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Application.Commands;

public record CreateUserInvestmentObjectsRequest(Guid UserId, string Name) : IRequest<Guid>;

internal sealed class CreateUserInvestmentObjectsHandler : IRequestHandler<CreateUserInvestmentObjectsRequest, Guid>
{
    private readonly IUserInvestmentObjectsFactory _userInvestmentObjectsFactory;
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectsRepository;

    public CreateUserInvestmentObjectsHandler(IUserInvestmentObjectsFactory userInvestmentObjectsFactory, IUserInvestmentObjectRepository userInvestmentObjectsRepository)
    {
        _userInvestmentObjectsFactory = userInvestmentObjectsFactory;
        _userInvestmentObjectsRepository = userInvestmentObjectsRepository;
    }

    public async Task<Guid> Handle(CreateUserInvestmentObjectsRequest request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);

        var userInvestmentObjects = await _userInvestmentObjectsFactory.CreateAsync(userId, request.Name, cancellationToken);
        await _userInvestmentObjectsRepository.AddAsync(userInvestmentObjects, cancellationToken);

        return userInvestmentObjects.Id;
    }
}
