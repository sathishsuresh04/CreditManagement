namespace CreditManagement.Domain.Primitives;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 4,
    Unauthorized = 5,
    Forbidden = 6,
}
