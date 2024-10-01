namespace CreditManagement.Domain.Abstractions;

public interface IAuditableEntity
{
    /// <summary>
    ///     Gets the creation date and time in UTC format.
    /// </summary>
    DateTime CreatedOnUtc { get; }

    /// <summary>
    ///     Gets  the last modified date and time in UTC format.
    /// </summary>
    DateTime? ModifiedOnUtc { get; }
}
