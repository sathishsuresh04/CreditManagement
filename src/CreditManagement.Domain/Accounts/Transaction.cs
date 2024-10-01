using Ardalis.GuardClauses;
using CreditManagement.Domain.Abstractions;
using CreditManagement.Domain.Primitives;

namespace CreditManagement.Domain.Accounts;

public class Transaction: Entity, IAuditableEntity
{
    private Transaction(DateTime dateTime, decimal amount,
        string? description,TransactionCategory category):base(Guid.NewGuid())
    {
        Date= Guard.Against.Default( dateTime);
        Amount = Guard.Against.NegativeOrZero(amount);
        Description = description;
        Category = category;
    }
    public Guid AccountId { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionCategory Category { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }

    public static Transaction Create(DateTime dateTime, decimal amount, string? description)
    {
        var category = CategorizeTransaction(description);
        var transaction = new Transaction(dateTime, amount, description, category);
        return transaction;
    }

    private static TransactionCategory CategorizeTransaction(string? description)
    {
        if (description == null) return TransactionCategory.Expense; // Default to Expense if no other category matches
        if (description.Contains("Salary", StringComparison.OrdinalIgnoreCase))
        {
            return TransactionCategory.Income;
        }

        return description.Contains("Transfer", StringComparison.OrdinalIgnoreCase) ? TransactionCategory.InternalTransfer : TransactionCategory.Expense; // Default to Expense if no other category matches
    }
}