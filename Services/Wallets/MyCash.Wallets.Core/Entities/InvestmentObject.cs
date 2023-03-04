using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Entities;

public class InvestmentObject
{
    public InvestmentObjectId Id { get; private set; } = null!;

    public InvestmentObjectName Name { get; private set; } = null!;

    public InvestmentObjectType Type { get; private set; } = null!;

    public HashSet<Transaction> Transactions { get; private set; } = new();

    private InvestmentObject()
    {
    }

    internal InvestmentObject(InvestmentObjectId id, InvestmentObjectName name, InvestmentObjectType type)
    {
        Id = id;
        Name = name;
        Type = type;
        Transactions = new HashSet<Transaction>();
    }

    internal void ChangeName(InvestmentObjectName name)
        => Name = name;

    internal void ChangeType(InvestmentObjectType type)
        => Type = type;

    internal void AddTransaction(Transaction transaction)
    {
        transaction.InvestmentObjectId = Id;
        Transactions.Add(transaction);
    }
}
