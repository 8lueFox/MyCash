using MyCash.WealthManager.Core.Types;

namespace MyCash.WealthManager.Core.ValueObjects;

public abstract class AggregateRoot
{
    private bool _versionIncremented = false;
    public AggregateId Id { get; protected set; } = null!;
    public int Version { get; protected set; }

    protected void IncrementVersion()
    {
        if (!_versionIncremented)
            Version++;
    }
}
