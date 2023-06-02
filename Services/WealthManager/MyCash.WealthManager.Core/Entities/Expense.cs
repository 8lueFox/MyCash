using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Expense : MoneyTransfer
{
    public ExpenseId Id { get; set; } = ExpenseId.Create();
    public virtual AggregateId FamilyId { get; set; } = null!;
}
