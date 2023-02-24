using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Policies;

public class StandardInvestmentObjectPolicy : IInvestmentObjectPolicy
{
    public bool CanBeAdded(IEnumerable<InvestmentObject> investments)
        => investments.Count() < 10;

    public bool CanBeApplied(string userPackage)
        => userPackage is UserPackage.Standard;
}
