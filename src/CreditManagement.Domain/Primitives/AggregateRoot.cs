using CreditManagement.Domain.Events;

namespace CreditManagement.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    ///     Represents the base class for aggregate roots in the domain.
    /// </summary>
    protected AggregateRoot(Guid id)
        : base(id)
    {
    }


    protected AggregateRoot()
    {
    }

    /// <summary>
    ///     Gets the collection of domain events associated with the aggregate root.
    /// </summary>
    /// <remarks>
    ///     This property provides a read-only view of the domain events that have been recorded
    ///     for the aggregate root. These events are typically used to track changes to the
    ///     aggregate state and can be processed by event handlers or other mechanisms.
    /// </remarks>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    ///     Clears all domain events from the aggregate root.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    ///     Adds a domain event to the aggregate root's collection of domain events.
    /// </summary>
    /// <param name="domainEvent">The domain event to be added.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
