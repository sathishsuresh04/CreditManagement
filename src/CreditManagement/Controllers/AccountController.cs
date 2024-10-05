using CreditManagement.Application.Accounts.CreateAccountTransactions;
using CreditManagement.Application.Accounts.GetAllAccounts;
using CreditManagement.Application.Transactions.GetAccountTransactions;
using CreditManagement.Application.Transactions.GetMonthlyTransactionReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditManagement.Controllers;

public class AccountController(IMediator mediator) : Controller
{
    /// <summary>
    /// Retrieves a list of all accounts asynchronously and returns the result as a view.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public async Task<IActionResult> IndexAsync()
    {
        var result = await mediator.Send(new GetAllAccountsQuery());
        if (result.IsSuccess) return View(result.Value);

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Handles the upload of a JSON file, processes its content to create account transactions asynchronously,
    /// and returns the appropriate result.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the upload and processing operation.</returns>
    [HttpPost]
    public async Task<IActionResult> UploadJsonFileAsync()
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        using var stream = new StreamReader(file.OpenReadStream());
        var jsonData = await stream.ReadToEndAsync();
        var command = new CreateAccountTransactionsCommand(jsonData);
        var result = await mediator.Send(command);

        if (result.IsSuccess) return RedirectToAction("Index");

        return BadRequest(result.Error);
    }

    //TODO : It should go into its own controller when we expand
    /// <summary>
    /// Retrieves a list of transactions for a specific account and returns the result as a view.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account whose transactions are to be retrieved.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public async Task<IActionResult> ViewTransactionsAsync(Guid accountId)
    {
        var result = await mediator.Send(new GetAccountTransactionsQuery(accountId));
        if (result.IsSuccess) return View(result.Value);

        return BadRequest(result.Error);
    }
    //TODO : It should go into its own controller when we expand
    /// <summary>
    /// Generates a monthly transaction report for a specific account for the given month and year.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account for which the report is generated.</param>
    /// <param name="month">The month for which the report is generated.</param>
    /// <param name="year">The year for which the report is generated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IActionResult"/> containing the monthly transaction report.</returns>
    [HttpGet("{accountId}/{month}/{year}")]
    public async Task<IActionResult> ReportsAsync(Guid accountId, int month, int year,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMonthlyTransactionReportQuery(accountId, month, year),
            cancellationToken);
        if (result.IsSuccess) return View(result.Value);

        return BadRequest(result.Error);
    }
}