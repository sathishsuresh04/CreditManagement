namespace CreditManagement.Domain.Abstractions;

public interface ISoftDeletableEntity
{
    /// <summary>
    ///     The DateTime (in UTC) when the entity was marked as deleted.
    /// </summary>
    DateTime? DeletedOnUtc { get; }

    /// <summary>
    ///     Indicates whether the entity is marked as deleted.
    /// </summary>
    bool Deleted { get; }
}