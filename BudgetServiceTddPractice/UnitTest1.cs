using FluentAssertions;
using NSubstitute;

namespace BudgetServiceTddPractice;

public class Tests
{
    private BudgetService _budgetService;
    private IBudgetRepo _budgetRepo;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void should_be_able_to_calculate_one_month()
    {
        _budgetRepo.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202301",
                Amount = 310
            }
        });
        var query = _budgetService.Query(
            new DateTime(2023, 1, 1),
            new DateTime(2023, 1, 31));
        query.Should().Be(310);
    }
}

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
        return budgets.Sum(x => x.Amount);
    }
}

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
}