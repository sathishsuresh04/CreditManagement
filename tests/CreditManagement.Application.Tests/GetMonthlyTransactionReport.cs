using CreditManagement.Application.Transactions.GetMonthlyTransactionReport;
using CreditManagement.Domain.Transactions;
using FluentAssertions;
using NSubstitute;

namespace CreditManagement.Application.Tests;

public class GetMonthlyTransactionReport
{
    private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();


    [Fact]
    public async Task GetMonthlyTransactionReport_ShouldReturnReport()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var year = 2023;
        var month = 5;
        var transactions = new List<Transaction>
        {
            Transaction.Create(new DateTime(year, month, 1), 1000m, "Salary", accountId),
            Transaction.Create(new DateTime(year, month, 15), 200m, "Groceries", accountId),
            Transaction.Create(new DateTime(year, month, 20), 500m, "Rent", accountId)
        };

        _transactionRepository.GetAccountTransactionsByMonthAndYearAsync(accountId, year, month).Returns(transactions);

        var handler = new GetMonthlyTransactionReportQueryHandler(_transactionRepository);
        var query = new GetMonthlyTransactionReportQuery(accountId, month, year);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.TotalIncome.Should().Be(1000m);
        result.Value.TotalExpenses.Should().Be(700m);
        result.Value.CategoryTransactionCounts.Should().HaveCount(2);
    }
}