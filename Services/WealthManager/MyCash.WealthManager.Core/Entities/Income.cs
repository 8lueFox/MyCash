using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Income
{
    public IncomeId Id { get; set; } = IncomeId.Create();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Value ValueNet { get; set; } = null!;
    public Value ValueGross { get; set; } = null!;
    public Date ReceiveDate { get; set; } = null!;
    public bool IsActive { get; set; }
    public MoneyTransferType IncomeType { get; set; } = MoneyTransferType.Default;
    public Period? Period { get; set; }
}
