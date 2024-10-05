using CreditManagement.Domain.Primitives;
using CreditManagement.Domain.Primitives.Result;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CreditManagement.Common.Minimal;

public static class ResultExtensions
{
    public static ProblemHttpResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess) throw new InvalidOperationException("Can't convert success result to problem");

        return TypedResults.Problem(
            result.Error.Message,
            statusCode: GetStatusCode(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                {
                    "errors", new[]
                    {
                        result.Error
                    }
                }
            });

        static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}