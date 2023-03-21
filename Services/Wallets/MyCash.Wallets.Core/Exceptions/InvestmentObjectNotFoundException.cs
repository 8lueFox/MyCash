namespace MyCash.Wallets.Core.Exceptions;

public class InvestmentObjectNotFoundException : CustomException
{
    public InvestmentObjectId Id { get; }

    public InvestmentObjectNotFoundException(InvestmentObjectId id)
        : base($"Investment object with id: {id} not found.")
        => Id = id;
}
