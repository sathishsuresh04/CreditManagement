using CreditManagement.Application.Transactions.GetAccountTransactions;
using CreditManagement.Domain.Transactions;
using FluentAssertions;
using NSubstitute;

namespace CreditManagement.Application.Tests;

public class GetAccountTransactions
{
    private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();

    [Fact]
    public async Task GetAccountTransactions_ShouldReturnTransactions()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var transactions = new List<Transaction>
        {
            Transaction.Create(DateTime.Now, 500m, "Salary", accountId),
            Transaction.Create(DateTime.Now, 100m, "Groceries", accountId)
        };

        _transactionRepository.GetAccountTransactionsByAccountIdAsync(accountId).Returns(transactions);

        var handler = new GetAccountTransactionsQueryHandler(_transactionRepository);
        var query = new GetAccountTransactionsQuery(accountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }
}