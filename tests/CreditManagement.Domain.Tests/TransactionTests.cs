using CreditManagement.Domain.Transactions;
using CreditManagement.Shared.Tests.Fixure;
using FluentAssertions;

namespace CreditManagement.Domain.Tests;

public class TransactionTests(TransactionTestFixture fixture) : IClassFixture<TransactionTestFixture>
{
    [Fact]
    public void Create_ShouldCreateTransactionWithCorrectProperties()
    {
        // Arrange
        var accountId = Guid.NewGuid();

        // Act
        var transaction = fixture.CreateTestTransaction(accountId, 1000m, "Salary payment");

        // Assert
        transaction.Should().NotBeNull();
        transaction.AccountId.Should().Be(accountId);
        transaction.Amount.Should().Be(1000m);
        transaction.Description.Should().Be("Salary payment");
        transaction.Category.Should().Be(TransactionCategory.Income);
        transaction.IsAnomalous.Should().BeFalse();
    }

    [Theory]
    [InlineData("Salary", TransactionCategory.Income)]
    [InlineData("Transfer", TransactionCategory.InternalTransfer)]
    [InlineData("Groceries", TransactionCategory.Expense)]
    [InlineData(null, TransactionCategory.Expense)]
    public void CategorizeTransaction_ShouldReturnCorrectCategory(string description,
        TransactionCategory expectedCategory)
    {
        // Arrange & Act
        var transaction = fixture.CreateTestTransaction(Guid.NewGuid(), 100m, description);

        // Assert
        transaction.Category.Should().Be(expectedCategory);
    }

    // Add more tests for SetAnomaly and other methods
}