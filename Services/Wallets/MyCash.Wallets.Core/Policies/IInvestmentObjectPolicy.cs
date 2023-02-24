using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Core.Policies;

public interface IInvestmentObjectPolicy
{
    bool CanBeApplied(string userPackage);
    bool CanBeAdded(IEnumerable<InvestmentObject> investments);
}
