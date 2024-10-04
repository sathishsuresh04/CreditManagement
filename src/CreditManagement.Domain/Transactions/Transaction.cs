using Ardalis.GuardClauses;
using CreditManagement.Domain.Abstractions;
using CreditManagement.Domain.Primitives;

namespace CreditManagement.Domain.Transactions;

public class Transaction : Entity, IAuditableEntity
{
    private const decimal AnomalousThreshold = 10000m;
    private Transaction()
    {
    }

    private Transaction(DateTime dateTime, decimal amount,
        string? description, TransactionCategory category,Guid accountId) : base(Guid.NewGuid())
    {
        Date = Guard.Against.Default(dateTime);
        Amount = Guard.Against.NegativeOrZero(amount);
        Description = description;
        Category = category;
        IsAnomalous = CheckForAnomaly(amount);
        AccountId = accountId;
    }

    public Guid AccountId { get; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    
    public bool IsAnomalous { get; private set; }
    public TransactionCategory Category { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }

    public static Transaction Create(DateTime dateTime, decimal amount, string? description,Guid accountId)
    {
        var category = CategorizeTransaction(description);
        var transaction = new Transaction(dateTime, amount, description, category,accountId);
        return transaction;
    }

    private static TransactionCategory CategorizeTransaction(string? description)
    {
        if (description == null) return TransactionCategory.Expense; // Default to Expense if no other category matches
        if (description.Contains("Salary", StringComparison.OrdinalIgnoreCase)) return TransactionCategory.Income;

        return description.Contains("Transfer", StringComparison.OrdinalIgnoreCase)
            ? TransactionCategory.InternalTransfer
            : TransactionCategory.Expense; // Default to Expense if no other category matches
    }
    private static bool CheckForAnomaly(decimal amount)
    {
        return amount > AnomalousThreshold;
    }

    /// <summary>
    /// Marks the transaction as anomalous.
    /// </summary>
    public void SetAnomaly()
    {
        IsAnomalous = true;
    }
}