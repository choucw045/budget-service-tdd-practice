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
        if (!IsValidDateRange(start, end))
            return 0m;

        return _budgetRepo.GetAll().Sum(x => x.GetEffectiveBudget(start, end));
    }

    private static bool IsValidDateRange(DateTime start, DateTime end)
    {
        return start <= end;
    }
}