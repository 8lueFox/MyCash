using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Factories;

public interface IFamilyFactory
{
    Task<Family> CreateAsync(UserId userId, FamilyName familyName, CancellationToken cancellationToken);
}
