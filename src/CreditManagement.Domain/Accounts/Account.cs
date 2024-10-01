using CreditManagement.Domain.Abstractions;
using CreditManagement.Domain.Primitives;

namespace CreditManagement.Domain.Accounts;

public class Account: AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    public readonly List<Transaction> _transactions = [];
    public string AccountHolder { get; private set; } = null!;
    public decimal Balance { get;  private set; }
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
}