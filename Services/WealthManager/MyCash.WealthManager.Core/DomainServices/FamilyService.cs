using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.DomainServices;

public class FamilyService : IFamilyService
{
    public Expense AddExpense(Family family, string Name, Value value, Date sendDate, bool isActive, MoneyTransferType expenseType, Period? period, string description)
    {
        var expense = new Expense
        {
            Id = ExpenseId.Create(),
            Name = Name,
            Value = value,
            SendDate = sendDate,
            IsActive = isActive,
            Period = period,
            Description = description
        };

        family.AddExpense(expense);
        return expense;
    }

    public Income AddIncome(Family family, string Name, Value valueNet, Value valueGross, Date receiveDate, bool isActive, MoneyTransferType incomeType, Period? period, string? Description)
    {
        var income = new Income
        {
            Id = IncomeId.Create(),
            Name = Name,
            Description = Description,
            IncomeType = incomeType,
            IsActive = isActive,
            Period = period,
            ReceiveDate = receiveDate,
            ValueGross = valueGross,
            ValueNet = valueNet
        };

        family.AddIncome(income);
        return income;
    }
}
