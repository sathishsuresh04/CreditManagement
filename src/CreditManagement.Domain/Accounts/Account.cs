using Ardalis.GuardClauses;
using CreditManagement.Domain.Abstractions;
using CreditManagement.Domain.Primitives;
using CreditManagement.Domain.Primitives.Result;
using CreditManagement.Domain.Transactions;

namespace CreditManagement.Domain.Accounts;

public class Account : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    private const decimal AnomalousThreshold = 10000m;
    private readonly List<Transaction> _transactions = [];

    private Account(string accountNumber, string accountHolder, decimal balance) : base(
        Guid.NewGuid())
    {
        AccountNumber = Guard.Against.NullOrWhiteSpace(accountNumber);
        AccountHolder = Guard.Against.NullOrWhiteSpace(accountHolder);
        Balance = Guard.Against.NegativeOrZero(balance);
    }

    public string AccountNumber { get; private set; }
    public string AccountHolder { get; private set; }
    public decimal Balance { get; private set; }
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }

    public static Account Create(string accountNumber, string accountHolder, decimal balance)
    {
        var invoice = new Account(accountNumber, accountHolder, balance);
        // invoice.AddDomainEvent(new AccountCreatedEvent(invoice)); // publish domain event
        return invoice;
    }

    public Result UpdateAccountHolder(string accountHolder)
    {
        if (accountHolder != AccountHolder) AccountHolder = accountHolder;

        return Result.Success();
    }

    public Result UpdateBalance(decimal balance)
    {
        Balance = balance;
        return Result.Success();
    }

    public Result IsAnomalyTransaction(DateTime date, decimal amount, string description)
    {
        var isDuplicate = _transactions.Any(t => t.Date == date && t.Amount == amount && t.Description == description);
        var isAboveThreshold = amount > AnomalousThreshold;

        if (isDuplicate || isAboveThreshold) return Result.Success();
        return Result.Failure(Error.Validation("", "Transaction is not anomalous"));
    }

    public Result AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);

        return Result.Success();
    }
}