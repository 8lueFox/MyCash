using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Repositories;

public interface IFamilyRepository
{
    Task CreateFamilyAsync(Family family, CancellationToken cancellationToken);
    Task UpdateFamilyAsync(Family family, CancellationToken cancellationToken);
    Task<Family> GetFamilyAsync(Guid familyId, CancellationToken cancellationToken);
}
