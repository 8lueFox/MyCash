using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Income : MoneyTransfer
{
    public IncomeId Id { get; set; } = IncomeId.Create();
    public Value ValueGross { get; set; } = null!;
    public virtual AggregateId FamilyId { get; set; } = null!;
}
