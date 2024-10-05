using CreditManagement.Shared.Tests.Fixure;
using FluentAssertions;

namespace CreditManagement.Domain.Tests;

public class AccountTests(AccountTestFixture fixture) : IClassFixture<AccountTestFixture>
{
    [Fact]
    public void Create_ShouldCreateAccountWithCorrectProperties()
    {
        // Arrange & Act
        var account = fixture.CreateTestAccount();

        // Assert
        account.Should().NotBeNull();
        account.AccountNumber.Should().Be("123456789");
        account.AccountHolder.Should().Be("John Doe");
        account.Balance.Should().Be(1000m);
        account.Transactions.Should().BeEmpty();
    }

    [Fact]
    public void IsAnomalyTransaction_ShouldDetectAnomalousTransaction()
    {
        // Arrange
        var account = fixture.CreateTestAccount();
        const decimal anomalousAmount = 15000m;

        // Act
        var result = account.IsAnomalyTransaction(DateTime.Now, anomalousAmount, "Large transfer");

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}