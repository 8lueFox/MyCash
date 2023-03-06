using Microsoft.Extensions.Logging;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Policies;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Infrastructure.DAL;

internal class WalletDataInitializator
{
    private readonly WalletDbContext _dbContext;
    private readonly ILogger<WalletDataInitializator> _logger;
    private readonly IEnumerable<IInvestmentObjectPolicy> _investmentObjectPolicies;

    public WalletDataInitializator(WalletDbContext dbContext, 
        ILogger<WalletDataInitializator> logger, 
        IEnumerable<IInvestmentObjectPolicy> investmentObjectPolicies)
    {
        _dbContext = dbContext;
        _logger = logger;
        _investmentObjectPolicies = investmentObjectPolicies;
    }

    internal async Task InitAsync(CancellationToken cancellationToken)
    {
        var user = new User(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Standard");
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);


        var userInvestmentObject1 = new UserInvestmentObjects(Guid.NewGuid(), user, "Agressive Wallet");
        var userInvestmentObject2 = new UserInvestmentObjects(Guid.NewGuid(), user, "Defensive Wallet");
        await _dbContext.UsersInvestmentObjects.AddRangeAsync(userInvestmentObject1, userInvestmentObject2);

        var investmentObject1 = new InvestmentObject(Guid.Parse("22222222-2222-2222-2222-222222222222"), "XYZ", InvestmentObjectType.Stock);
        var investmentObject2 = new InvestmentObject(Guid.Parse("33333333-3333-3333-3333-333333333333"), "YZX", InvestmentObjectType.Stock);
        var investmentObject3 = new InvestmentObject(Guid.NewGuid(), "ZXY", InvestmentObjectType.Stock);
        var investmentObject4 = new InvestmentObject(Guid.NewGuid(), "YXZ", InvestmentObjectType.Stock);
        var investmentObject5 = new InvestmentObject(Guid.NewGuid(), "XZY", InvestmentObjectType.Stock);

        userInvestmentObject1.AddInvestmentObject(investmentObject1, _investmentObjectPolicies);
        userInvestmentObject1.AddInvestmentObject(investmentObject2, _investmentObjectPolicies);
        userInvestmentObject1.AddInvestmentObject(investmentObject3, _investmentObjectPolicies);
        userInvestmentObject2.AddInvestmentObject(investmentObject4, _investmentObjectPolicies);
        userInvestmentObject2.AddInvestmentObject(investmentObject5, _investmentObjectPolicies);

        var transacton1 = new Transaction(Guid.NewGuid(), new Amount(1, "GBP", 10.3M), new Date(new DateTimeOffset(2022, 10, 5, 0, 0, 0, TimeSpan.Zero)));
        var transacton2 = new Transaction(Guid.NewGuid(), new Amount(2, "GBP", 11.5M), new Date(new DateTimeOffset(2022, 11, 5, 0, 0, 0, TimeSpan.Zero)));
        var transacton3 = new Transaction(Guid.NewGuid(), new Amount(7, "USD", 593.5M), new Date(new DateTimeOffset(2022, 11, 6, 0, 0, 0, TimeSpan.Zero)));
        var transacton4 = new Transaction(Guid.NewGuid(), new Amount(1, "USD", 907M), new Date(new DateTimeOffset(2022, 11, 7, 0, 0, 0, TimeSpan.Zero)));

        userInvestmentObject1.AddTransaction(investmentObject1.Id, transacton1);
        userInvestmentObject1.AddTransaction(investmentObject1.Id, transacton2);
        userInvestmentObject1.AddTransaction(investmentObject2.Id, transacton3);
        userInvestmentObject1.AddTransaction(investmentObject2.Id, transacton4);
            
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

