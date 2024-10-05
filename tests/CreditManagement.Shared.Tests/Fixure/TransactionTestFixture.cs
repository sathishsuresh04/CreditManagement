using CreditManagement.Domain.Transactions;

namespace CreditManagement.Shared.Tests.Fixure;

public class TransactionTestFixture
{
    public Transaction CreateTestTransaction(Guid accountId, decimal amount = 100m,
        string description = "Test transaction")
    {
        return Transaction.Create(DateTime.Now, amount, description, accountId);
    }
}