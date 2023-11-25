namespace BudgetServiceTddPractice;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();
        return budgets.Sum(x => x.ValidDays(start, end) * x.Amount / DateTime.DaysInMonth(x.GetYear(), x.GetMonth()));
    }
}