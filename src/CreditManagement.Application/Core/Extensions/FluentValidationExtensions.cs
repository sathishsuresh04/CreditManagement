using FluentValidation;
using CreditManagement.Domain.Primitives;

namespace CreditManagement.Application.Core.Extensions;

public static class FluentValidationExtensions
{
    /// <summary>
    ///     Associates a custom error with the validation rule.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated.</typeparam>
    /// <param name="rule">The validation rule to which the error will be associated.</param>
    /// <param name="error">The custom error to be displayed when validation fails.</param>
    /// <returns>Returns the rule builder options with the specified error code and message.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided error is null.</exception>
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error
    )
    {
        if (error is null) throw new ArgumentNullException(nameof(error), "The error is required");

        return rule.WithErrorCode(error.Code).WithMessage(error.Message);
    }
}
