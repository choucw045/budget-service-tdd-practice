using FluentAssertions;
using NSubstitute;

namespace BudgetServiceTddPractice;

public class Tests
{
    private BudgetService _budgetService = null!;
    private IBudgetRepo _budgetRepo = null!;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void should_be_able_to_calculate_one_month()
    {
        GivenBudgets(
            CreateBudget(2023, 1, 310)
        );
        var query = _budgetService.Query(
            new DateTime(2023, 1, 1),
            new DateTime(2023, 1, 31));
        query.Should().Be(310);
    }

    [Test]
    public void should_be_able_to_calculate_one_day()
    {
        GivenBudgets(CreateBudget(2023, 1, 310));
        var query = _budgetService.Query(
            new DateTime(2023, 1, 1),
            new DateTime(2023, 1, 1));
        query.Should().Be(10);
    }

    [Test]
    public void should_return_0_if_period_invalid()
    {
        GivenBudgets(
            CreateBudget(2023, 1, 310)
        );
        var query = _budgetService.Query(
            new DateTime(2023, 1, 4),
            new DateTime(2023, 1, 1));
        query.Should().Be(0);
    }

    [Test]
    public void should_be_able_to_handle_cross_month()
    {
        GivenBudgets(
            CreateBudget(2023, 1, 310),
            CreateBudget(2023, 2, 28)
        );
        var query = _budgetService.Query(
            new DateTime(2023, 1, 4),
            new DateTime(2023, 2, 1));
        query.Should().Be(280 + 1);
    }


    private void GivenBudgets(params Budget[] budget)
    {
        _budgetRepo.GetAll().Returns(budget.ToList());
    }

    private static Budget CreateBudget(int year, int month, int amount)
    {
        return new()
        {
            YearMonth = $"{year:0000}{month:00}",
            Amount = amount
        };
    }
}