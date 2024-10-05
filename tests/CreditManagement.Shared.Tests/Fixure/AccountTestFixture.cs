using CreditManagement.Domain.Accounts;
using CreditManagement.Domain.Transactions;

namespace CreditManagement.Shared.Tests.Fixure;

public class AccountTestFixture
{
    public Account CreateTestAccount()
    {
        return Account.Create("123456789", "John Doe", 1000m);
    }

    public Transaction CreateTestTransaction(Guid accountId, decimal amount = 100m)
    {
        return Transaction.Create(DateTime.Now, amount, "Test transaction", accountId);
    }
}