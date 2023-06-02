using Microsoft.EntityFrameworkCore;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

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

    public async Task<IList<Family>> GetAllFamiliesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext
            .Families
            .Include(x => x.Incomes)
            .Include(x => x.Expenses)
            .Include(x => x.Incomes)
            .Include(x => x.Balance)
            .Where(x => x.Incomes.Any(x => x.TransferType == MoneyTransferType.Periodical) || x.Expenses.Any(x => x.TransferType == MoneyTransferType.Periodical))
            .ToListAsync();
    }

    public async Task<Family> GetFamilyAsync(AggregateId familyId, CancellationToken cancellationToken)
    {
        return await _families
            .Include(x => x.Expenses)
            .Include(x => x.Incomes)
            .Include(x => x.Settings)
            .Include(x => x.Balance)
            .ThenInclude(x => x.Events)
            .SingleOrDefaultAsync(x => x.Id == familyId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateFamilyAsync(Family family, CancellationToken cancellationToken)
    {
        _families.Update(family);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
