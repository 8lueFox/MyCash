using MyCash.Micro.Time;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Policies;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.DomainServices;

public class UserInvestmentObjectsService : IUserInvestmentObjectsService
{
    private readonly IEnumerable<IInvestmentObjectPolicy> _investmentObjectPolicies;
    private readonly IClock _clock;

    public UserInvestmentObjectsService(IEnumerable<IInvestmentObjectPolicy> investmentObjectPolicies, IClock clock)
    {
        _investmentObjectPolicies = investmentObjectPolicies;
        _clock = clock;
    }

    public InvestmentObject AddNew(UserInvestmentObjects userInvestmentObjects, InvestmentObjectName name, InvestmentObjectType type)
    {
        var io = new InvestmentObject(InvestmentObjectId.Create(), name, type);

        userInvestmentObjects.AddInvestmentObject(io, _investmentObjectPolicies);

        return io;
    }

    public Transaction AddTransaction(UserInvestmentObjects userInvestmentObjects, InvestmentObjectId investmentObjectId, Amount amount, Date date)
    {
        var transaction = new Transaction(TransactionId.Create(), amount, date is null ? new Date(_clock.Current()) : date);

        userInvestmentObjects.AddTransaction(investmentObjectId, transaction);

        return transaction;
    }
}
