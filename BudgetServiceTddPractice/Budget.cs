namespace BudgetServiceTddPractice;

public class Budget
{
    public string YearMonth { get; set; } = null!;
    public int Amount { get; set; }

    public int GetEffectiveBudget(DateTime start, DateTime end)
    {
        return ValidDays(start, end) * Amount / DaysInMonth();
    }

    private int ValidDays(DateTime inputStart, DateTime inputEnd)
    {
        var firstDateOfMonth = new DateTime(GetYear(), GetMonth(), 1);
        var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

        var spanStart = inputStart > firstDateOfMonth ? inputStart : firstDateOfMonth;
        var spanEnd = inputEnd < lastDateOfMonth ? inputEnd : lastDateOfMonth;
        if (spanEnd >= spanStart)
            return spanEnd.Day - spanStart.Day + 1;
        return 0;
    }

    private int DaysInMonth()
    {
        return DateTime.DaysInMonth(GetYear(), GetMonth());
    }

    private int GetYear()
    {
        return Convert.ToInt32(YearMonth.Substring(0, 4));
    }

    private int GetMonth()
    {
        return Convert.ToInt32(YearMonth.Substring(4, 2));
    }
}