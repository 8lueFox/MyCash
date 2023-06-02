using MyCash.BackgroundWorker.Application.Services.Interfaces;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.BackgroundWorker.Application.Services;

public sealed class BalanceOrganizator : IBalanceOrganizator
{
    private readonly IFamilyRepository _familyRepository;

    public BalanceOrganizator(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public async Task CheckAllFamilies(CancellationToken cancellationToken)
    {
        var families = await _familyRepository.GetAllFamiliesAsync(cancellationToken);
        var options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 10
        };

        Parallel.ForEach(families, options, f =>
        {
            var operations = new List<MoneyTransfer>();
            operations.AddRange(f.Expenses.Where(x => x.TransferType == MoneyTransferType.Periodical).ToList());
            operations.AddRange(f.Incomes.Where(x => x.TransferType == MoneyTransferType.Periodical).ToList());

            foreach(var o in operations)
            {
                if(o.OperationDate < Date.Now)
                {
                    f.AddBalanceEvent(new BalanceEvent
                    {
                        Value = o.Value,
                        Name = o.Name,
                        EventDate = Date.Now,
                        BalanceEventType = o is Income ? BalanceEventType.Income : BalanceEventType.Expense
                    });
                    o.OperationDate = o.Period!.Days switch
                    {
                        Period.Daily => o.OperationDate.Value.AddDays(1),
                        Period.Weekly => o.OperationDate.Value.AddDays(7),
                        Period.Monthly => o.OperationDate.Value.AddMonths(1),
                        Period.SemiAnnually => o.OperationDate.Value.AddMonths(6),
                        Period.Annually => o.OperationDate.Value.AddYears(1),
                        _ => throw new NotImplementedException(),
                    };
                }
            }
        });

        await _familyRepository.SaveChangesAsync(cancellationToken);
    }
}
