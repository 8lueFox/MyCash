namespace MyCash.BackgroundWorker.Application.Services.Interfaces;

public interface IBalanceOrganizator
{
    Task CheckAllFamilies(CancellationToken cancellationToken);
}
