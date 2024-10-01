using CreditManagement.Application.Core.Exceptions;
using CreditManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Common.Exception;

public class GlobalExceptionHandler(IHostEnvironment environment)

    : IExceptionHandler

{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken
    )
    {
        httpContext.Response.ContentType = "application/problem+json";

        if (httpContext.RequestServices.GetService<IProblemDetailsService>() is
            not
            {
            } problemDetailsService)
        {
            return true;
        }

        (string Detail, string Title, int StatusCode) details = exception switch
                                                                {
                                                                    ValidationException validationException=>
                                                                        (
                                                                            string.Join(
                                                                                ", ",
                                                                                validationException.Errors.Select(e => e.Message)),
                                                                            exception.GetType().Name,
                                                                            httpContext.Response.StatusCode =
                                                                                StatusCodes.Status400BadRequest
                                                                        ),
                                                                    DomainException domainException => (

                                                                                domainException.Error.Message,
                                                                            domainException.GetType().Name,
                                                                            httpContext.Response.StatusCode =
                                                                                StatusCodes.Status400BadRequest
                                                                        ),
                                                                    DbUpdateConcurrencyException =>
                                                                        (
                                                                            exception.Message,
                                                                            exception.GetType().Name,
                                                                            httpContext.Response.StatusCode =
                                                                                StatusCodes.Status409Conflict
                                                                        ),
                                                                    _ =>
                                                                        (
                                                                            exception.Message,
                                                                            exception.GetType().Name,
                                                                            httpContext.Response.StatusCode =
                                                                                StatusCodes
                                                                                    .Status500InternalServerError
                                                                        ),
                                                                };


        var problem = new ProblemDetailsContext
                      {
                          HttpContext = httpContext,
                          ProblemDetails =
                          {
                              Title = details.Title, Detail =
                                                                            details.Detail, Status = details.StatusCode,
                          },
                      };

        if (environment.IsDevelopment())
        {
            problem.ProblemDetails.Extensions.Add(
                "exception",
                exception.ToString());
        }

        await problemDetailsService.WriteAsync(problem);

        return true;
    }
}
