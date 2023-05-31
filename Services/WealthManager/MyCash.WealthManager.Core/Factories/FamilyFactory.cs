using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Factories;

public class FamilyFactory : IFamilyFactory
{
    public Task<Family> CreateAsync(UserId userId, FamilyName familyName, FamilySettings familySettings, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Family(AggregateId.Create(), userId, familyName, familySettings));
    }
}
