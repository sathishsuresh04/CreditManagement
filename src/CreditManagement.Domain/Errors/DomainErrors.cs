using CreditManagement.Domain.Primitives;

namespace CreditManagement.Domain.Errors;

public static class DomainErrors
{
    /// <summary>
    ///     Contains the attendee errors.
    /// </summary>
    public static class Account
    {
        public static Error NotFound => Error.NotFound(
            "Account.NotFound",
            "The Account with the specified identifier was not found.");
    }
}