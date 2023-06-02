using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.DomainServices;

internal class FamilyService : IFamilyService
{
    public Expense AddExpense(Family family, string Name, Value value, Date sendDate, bool isActive, MoneyTransferType expenseType, Period? period, string? description)
    {
        var expense = new Expense
        {
            Id = ExpenseId.Create(),
            Name = Name,
            Value = value,
            OperationDate = sendDate,
            IsActive = isActive,
            Period = period,
            Description = description,
            TransferType = expenseType
        };

        family.AddExpense(expense);
        if (expenseType.Value == MoneyTransferType.Disposable)
            family.AddBalanceEvent(new BalanceEvent
            {
                Name = Name,
                Value = value,
                BalanceEventType = BalanceEventType.Expense
            });
        return expense;
    }

    public Income AddIncome(Family family, string Name, Value valueNet, Value valueGross, Date receiveDate, bool isActive, MoneyTransferType incomeType, Period? period, string? description)
    {
        var income = new Income
        {
            Id = IncomeId.Create(),
            Name = Name,
            Description = description,
            TransferType = incomeType,
            IsActive = isActive,
            Period = period,
            OperationDate = receiveDate,
            ValueGross = valueGross,
            Value = valueNet
        };

        family.AddIncome(income);
        if (incomeType.Value == MoneyTransferType.Disposable)
            family.AddBalanceEvent(new BalanceEvent
            {
                Name = Name,
                Value = valueNet
            });
        return income;
    }

    public void DeleteExpense(Family family, Guid expenseId)
        => family.DeleteExpense(expenseId);

    public void DeleteIncome(Family family, Guid incomeId)
        => family.DeleteIncome(incomeId);
}
