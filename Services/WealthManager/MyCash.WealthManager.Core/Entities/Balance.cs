using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Balance : AggregateRoot
{
    private readonly HashSet<BalanceEvent> _events = new();

    public AggregateId FamilyId { get; set; }
    public  Family Family { get; set; } = null!;
    public Value Value { get; set; } = null!;

    public IEnumerable<BalanceEvent> Events => _events;

    public Balance()
    {
    }

    public Balance(string currency, AggregateId familyId)
    {
        Value = new Value(0, currency);
        Id = AggregateId.Create();
        FamilyId = familyId;
        IncrementVersion();
    }

    public void AddEvent(BalanceEvent @event)
    {
        @event.BalanceId = Id;
        _events.Add(@event);
        Value.Count += @event.BalanceEventType == BalanceEventType.Income ? @event.Value.Count : -@event.Value.Count;
    }
}
