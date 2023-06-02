using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public abstract class MoneyTransfer
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Date OperationDate { get; set; } = Date.Now;
    public Value Value { get; set; } = null!;
    public bool IsActive { get; set; }
    public MoneyTransferType TransferType { get; set; } = MoneyTransferType.Default;
    public Period? Period { get; set; }
}
