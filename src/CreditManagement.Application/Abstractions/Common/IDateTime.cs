namespace CreditManagement.Application.Abstractions.Common;

public interface IDateTime
{
    /// <summary>
    ///     Gets the current date and time in Coordinated Universal Time (UTC).
    /// </summary>
    /// <returns>
    ///     A <see cref="DateTime" /> object that is set to the current date and time in UTC.
    /// </returns>
    DateTime UtcNow { get; }
}
