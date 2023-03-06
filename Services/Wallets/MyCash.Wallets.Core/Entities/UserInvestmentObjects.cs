using MyCash.Wallets.Core.Policies;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Entities;

public class UserInvestmentObjects : AggregateRoot
{
    private readonly UserPackage _userPackage = UserPackage.None;
    private readonly HashSet<InvestmentObject> _investmentObjects = new();

    public UserId UserId { get; private set; } = null!;
    public UserInvestmentObjectName UserInvestmentObjectName { get; private set; } = null!;
    public IEnumerable<InvestmentObject> InvestmentObjects => _investmentObjects;

    private UserInvestmentObjects()
    {
    }

    public UserInvestmentObjects(AggregateId id, User user, UserInvestmentObjectName userInvestmentObjectName)
    {
        Id = id;
        UserId = user.Id;
        _userPackage = user.UserPackage;
        UserInvestmentObjectName = userInvestmentObjectName;
        IncrementVersion();
    }

    public void AddInvestmentObject(InvestmentObject io, IEnumerable<IInvestmentObjectPolicy> policies)
    {
        var policy = policies.SingleOrDefault(p => p.CanBeApplied(_userPackage));

        if (policy is null)
            throw new NoInvestmentObjectPolicyFoundException(_userPackage);

        if (!policy.CanBeAdded(_investmentObjects))
            throw new CannotMakeInvestmentObjectException(io.Name);

        _investmentObjects.Add(io);
        IncrementVersion();
    }

    public void AddTransaction(InvestmentObjectId investmentObjectId, Transaction transaction)
    {
        var io = _investmentObjects.SingleOrDefault(x => x.Id == investmentObjectId);

        if (io is null)
            throw new InvestmentObjectNotFoundException(investmentObjectId);

        io.AddTransaction(transaction);
        IncrementVersion();
    }
}
