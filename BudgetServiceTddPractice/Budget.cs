namespace BudgetServiceTddPractice;

public class Budget
{
    public string YearMonth { get; set; } = null!;
    public int Amount { get; set; }

    public int ValidDays(DateTime inputStart, DateTime inputEnd)
    {
        var firstDateOfMonth = new DateTime(GetYear(), GetMonth(), 1);
        var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

        var spanStart = inputStart > firstDateOfMonth ? inputStart : firstDateOfMonth;
        var spanEnd = inputEnd < lastDateOfMonth ? inputEnd : lastDateOfMonth;
        return spanEnd.Day - spanStart.Day + 1;
    }

    public int GetYear()
    {
        return Convert.ToInt32(YearMonth.Substring(0, 4));
    }

    public int GetMonth()
    {
        return Convert.ToInt32(YearMonth.Substring(4, 2));
    }

    public int GetEffectiveBudget(DateTime start, DateTime end)
    {
        return ValidDays(start, end) * Amount / DateTime.DaysInMonth(GetYear(), GetMonth());
    }
}