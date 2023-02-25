using MyCash.Micro.Exceptions;

namespace MyCash.Wallets.Core.Exceptions;

internal class InvalidInvestmentObjectException : CustomException
{
    public object Name { get; }

    public InvalidInvestmentObjectException(object name)
        : base($"Investment object {name} is invalid.")
        => Name = name;
}