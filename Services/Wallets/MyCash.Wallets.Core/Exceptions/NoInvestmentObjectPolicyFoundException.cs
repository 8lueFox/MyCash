using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Exceptions;

public class NoInvestmentObjectPolicyFoundException : CustomException
{
    public UserPackage UserPackage { get; }

    public NoInvestmentObjectPolicyFoundException(string userPackage)
        : base($"No investment object policy found for package: {userPackage}")
        => UserPackage = userPackage;
}
