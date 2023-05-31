using Microsoft.EntityFrameworkCore;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Infrastructure.DAL.Repositories;

internal class FamilyRepository : IFamilyRepository
{
    private readonly DbSet<Family> _families;
    private readonly WealthDbContext _dbContext;

    public FamilyRepository(WealthDbContext dbContext)
    {
        _dbContext = dbContext;
        _families = dbContext.Families;
    }

    public async Task CreateFamilyAsync(Family family, CancellationToken cancellationToken)
    {
        await _families.AddAsync(family, cancellationToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Family> GetFamilyAsync(Guid familyId, CancellationToken cancellationToken)
    {
        return await _families
            .Include(x => x.Expenses)
            .Include(x => x.Incomes)
            .Include(x => x.Settings)
            .SingleOrDefaultAsync(x => x.Id.Value == familyId, cancellationToken);
    }

    public async Task UpdateFamilyAsync(Family family, CancellationToken cancellationToken)
    {
        _families.Update(family);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
