using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Expense
{
    public ExpenseId Id { get; set; } = ExpenseId.Create();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Value Value { get; set; } = null!;
    public Date SendDate { get; set; } = null!;
    public bool IsActive { get; set; }
    public MoneyTransferType ExpenseType { get; set; } = MoneyTransferType.Default;
    public Period? Period { get; set; }

    public virtual AggregateId FamilyId { get; set; } = null!;
}
