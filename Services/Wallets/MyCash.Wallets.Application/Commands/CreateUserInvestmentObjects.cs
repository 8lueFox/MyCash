using MediatR;
using MyCash.Wallets.Core.Factories;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Application.Commands;

public record CreateUserInvestmentObjectsRequest(Guid userId, string Name) : IRequest<Guid>;

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
        var userId = new UserId(request.userId);

        var userInvestmentObjects = await _userInvestmentObjectsRepository.GetUserInvestmentObjectsAsync(userId, cancellationToken);

        if (userInvestmentObjects is not null)
            return userInvestmentObjects.Id;

        userInvestmentObjects = await _userInvestmentObjectsFactory.CreateAsync(userId, request.Name, cancellationToken);
        await _userInvestmentObjectsRepository.AddAsync(userInvestmentObjects, cancellationToken);

        return userInvestmentObjects.Id;
    }
}
