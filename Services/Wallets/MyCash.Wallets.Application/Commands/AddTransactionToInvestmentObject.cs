using MediatR;
using MyCash.Wallets.Core.DomainServices;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Application.Commands;

public record AddTransactionToInvestmentObjectRequest(
    Guid UserInvestmentObjectsId,
    Guid InvestmentObjectId, 
    decimal Count, decimal Price, string Currency, 
    DateTimeOffset Date) : IRequest;

internal class AddTransactionToInvestmentObjectHandler : IRequestHandler<AddTransactionToInvestmentObjectRequest>
{
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectsRepository;
    private readonly IUserInvestmentObjectsService _userInvestmentObjectsService;

    public AddTransactionToInvestmentObjectHandler(IUserInvestmentObjectRepository userInvestmentObjectsRepository,
        IUserInvestmentObjectsService userInvestmentObjectsService)
    {
        _userInvestmentObjectsRepository = userInvestmentObjectsRepository;
        _userInvestmentObjectsService = userInvestmentObjectsService;
    }

    public async Task Handle(AddTransactionToInvestmentObjectRequest request, CancellationToken cancellationToken)
    {
        var userInvestmentObjects = await _userInvestmentObjectsRepository.GetUserInvestmentObjectAsync(request.UserInvestmentObjectsId, cancellationToken);

        if (userInvestmentObjects is null)
            return;

        var amount = new Amount(request.Count, request.Currency, request.Price);
        var date = new Date(request.Date);

        _userInvestmentObjectsService.AddTransaction(userInvestmentObjects, request.InvestmentObjectId, amount, date);
        await _userInvestmentObjectsRepository.UpdateAsync(userInvestmentObjects, cancellationToken);

    }
}
