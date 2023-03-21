using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Exceptions;

public class CannotMakeInvestmentObjectException : CustomException
{
    public InvestmentObjectName Name { get; }

    public CannotMakeInvestmentObjectException(InvestmentObjectName name)
        :base($"Cannot add investment object to wallet with name: {name} due to investment object policy.")
        => Name = name;
}
