using Ardalis.GuardClauses;

namespace CreditManagement.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    ///     Represents the base class for entities in the domain.
    /// </summary>
    /// <remarks>
    ///     This class provides a base implementation for entities, enforcing a standard equality and identity mechanism.
    /// </remarks>
    protected Entity(Guid id)
        : this()
    {
        Guard.Against.NullOrEmpty(id, nameof(id), "Id is required ");
        Id = id;
    }

    protected Entity()
    {
    }

    public Guid Id { get; }

    /// <inheritdoc />
    public bool Equals(Entity other)
    {
        if (other is null) return false;

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is null) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        if (!(obj is Entity other)) return false;

        if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}