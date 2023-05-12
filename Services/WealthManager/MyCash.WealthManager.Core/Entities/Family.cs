using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Family : AggregateRoot
{
    private readonly HashSet<Expense> _expenses = new();
    private readonly HashSet<Income> _incomes = new();

    public UserId UserId { get; private set; } = null!;
    public FamilyName FamilyName { get; set; } = null!;
    public FamilySettings Settings { get; set; } = new();

    public Family(AggregateId id, UserId userId, FamilyName familyName)
    {
        Id = id;
        UserId = userId;
        FamilyName = familyName;
        IncrementVersion();
    }

    public void AddExpense(Expense expense)
    {
        if (expense is null)
            return;

        _expenses.Add(expense);
    }

    public void AddIncome(Income income)
    {
        if (income is null)
            return;

        _incomes.Add(income);
    }

    public void SetMonthlyExpenses(Value value)
    {
        Settings.MonthyExpenses = value;
    }

    public void DeleteExpense(Guid expenseId)
        => _expenses.RemoveWhere(x => x.Id.Value == expenseId);

    public void DeleteIncome(Guid incomeId)
        => _incomes.RemoveWhere(x => x.Id.Value == incomeId);
}
