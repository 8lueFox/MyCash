using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Entities;

public class Transaction
{
    public TransactionId Id { get; private set; } = null!;

    public Amount Amount { get; set; } = null!;

    public Date Date { get; set; } = null!;

    public InvestmentObjectId InvestmentObjectId { get; set; } = null!;
    public virtual InvestmentObject InvestmentObject { get; set; } = null!;

    private Transaction()
    {
    }

    internal Transaction(TransactionId id, Amount amount, Date date)
    {
        Id = id;
        Amount = amount;
        Date = date;
    }

    internal void ChangeAmount(Amount amount)
        => Amount = amount;

    internal void ChangeDate(Date date)
        => Date = date;
}