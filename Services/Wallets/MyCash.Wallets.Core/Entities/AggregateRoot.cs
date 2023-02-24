namespace MyCash.Wallets.Core.Entities;

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
