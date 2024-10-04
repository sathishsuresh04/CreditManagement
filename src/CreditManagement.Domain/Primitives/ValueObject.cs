namespace CreditManagement.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <inheritdoc />
    public bool Equals(ValueObject other)
    {
        return !(other is null) && GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (GetType() != obj.GetType()) return false;

        if (!(obj is ValueObject valueObject)) return false;

        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (var obj in GetAtomicValues()) hashCode.Add(obj);

        return hashCode.ToHashCode();
    }

    /// <summary>
    ///     Retrieves the atomic values that make up the value object.
    /// </summary>
    /// <return>
    ///     An enumerable collection of objects representing the atomic values of the value object.
    /// </return>
    protected abstract IEnumerable<object> GetAtomicValues();
}