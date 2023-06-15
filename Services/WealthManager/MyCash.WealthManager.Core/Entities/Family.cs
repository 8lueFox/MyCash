using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Core.Entities;

public class Family : AggregateRoot
{
    private readonly HashSet<Expense> _expenses = new();
    private readonly HashSet<Income> _incomes = new();

    public UserId UserId { get; private set; } = null!;
    public AggregateId BalanceId { get; set; }
    public FamilyName FamilyName { get; set; } = null!;
    public FamilySettings Settings { get; set; } = new();
    public Balance Balance { get; set; }
    public IEnumerable<Expense> Expenses => _expenses;
    public IEnumerable<Income> Incomes => _incomes;
    public IEnumerable<User> Users { get; set; }

    public Family()
    {
    } 

    public Family(AggregateId id, UserId userId, FamilyName familyName, FamilySettings settings)
    {
        Id = id;
        UserId = userId;
        FamilyName = familyName;
        Settings = settings;
        IncrementVersion();
        Balance = new Balance(settings.Currency, Id);
        BalanceId = Balance.Id;
    }

    
    public decimal GetSumOfExpenses(string currency)
        => _expenses.Sum(x => x.Value.GetValueInSpecificCurrency(currency));

    public decimal GetSumOfIncomes(string currency)
        => _incomes.Sum(x => x.Value.GetValueInSpecificCurrency(currency));

    public void AddExpense(Expense expense)
    {
        if (expense is null)
            return;

        expense.FamilyId = Id;
        _expenses.Add(expense);
    }

    public void AddIncome(Income income)
    {
        if (income is null)
            return;
        income.FamilyId = Id;
        _incomes.Add(income);
    }

    public void SetExpectedMonthyExpenses(decimal value)
    {
        Settings.ExpectedMonthyExpenses = value;
    }

    public void DeleteExpense(Guid expenseId)
        => _expenses.RemoveWhere(x => x.Id.Value == expenseId);

    public void DeleteIncome(Guid incomeId)
        => _incomes.RemoveWhere(x => x.Id.Value == incomeId);

    public void AddBalanceEvent(BalanceEvent balanceEvent)
        => Balance.AddEvent(balanceEvent);
}
