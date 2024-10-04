using CreditManagement.Domain.Primitives;

namespace CreditManagement.Domain.Exceptions;

// ReSharper disable once ClassNeverInstantiated.Global
#pragma warning disable RCS1194
public class DomainException : Exception
#pragma warning restore RCS1194
{
    /// <summary>
    ///     Represents an exception specific to domain logic errors.
    /// </summary>
    public DomainException(Error error)
        : base(error.Message)
    {
        Error = error;
    }

    /// <summary>
    ///     Gets the <see cref="Error" /> representing the domain-specific error encountered.
    /// </summary>
    public Error Error { get; }
}