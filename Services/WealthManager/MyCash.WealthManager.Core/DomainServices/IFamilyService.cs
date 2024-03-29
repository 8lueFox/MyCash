﻿using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.DomainServices;

public interface IFamilyService
{
    Expense AddExpense(Family family, string Name, Value value, Date sendDate, bool isActive, MoneyTransferType expenseType, Period? period, string? description);
    Income AddIncome(Family family, string Name, Value valueNet, Value valueGross, Date receiveDate, bool isActive, MoneyTransferType incomeType, Period? period, string? description);

    void DeleteExpense(Family family, Guid expenseId);
    void DeleteIncome(Family family, Guid incomeId);
}
