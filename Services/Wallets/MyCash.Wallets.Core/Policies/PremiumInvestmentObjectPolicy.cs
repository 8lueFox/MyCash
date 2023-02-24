using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Policies;

public class PremiumInvestmentObjectPolicy : IInvestmentObjectPolicy
{
    public bool CanBeAdded(IEnumerable<InvestmentObject> investments)
        => investments.Count() < 50;

    public bool CanBeApplied(string userPackage)
        => userPackage == UserPackage.Premium;
}
