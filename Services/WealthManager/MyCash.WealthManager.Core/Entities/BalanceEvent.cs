using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class BalanceEvent
{
    public BalanceEventId Id { get; set; } = BalanceEventId.Create();
    public string Name { get; set; } = string.Empty;
    public Value Value { get; set; } = null!;
    public Balance Balance { get; set; }

    public virtual AggregateId BalanceId { get; set; } = null!;
}