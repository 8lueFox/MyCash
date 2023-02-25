using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.DomainServices;

public interface IUserInvestmentObjectsService
{
    InvestmentObject AddNew(UserInvestmentObjects userInvestmentObjects, InvestmentObjectName name, InvestmentObjectType type);
    Transaction AddTransaction(UserInvestmentObjects userInvestmentObjects, InvestmentObjectId investmentObjectId,
        Amount amount, Date date);
}
