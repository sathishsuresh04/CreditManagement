using CreditManagement.Application.Accounts.CreateAccountTransactions;
using CreditManagement.Application.Accounts.GetAllAccounts;
using CreditManagement.Application.Transactions.GetAccountTransactions;
using CreditManagement.Application.Transactions.GetMonthlyTransactionReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditManagement.Controllers;

public class AccountController(IMediator mediator) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        var result = await mediator.Send(new GetAllAccountsQuery());
        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return BadRequest(result.Error);

    }

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

    public async Task<IActionResult> ViewTransactionsAsync(Guid accountId)
    {
        var result = await mediator.Send(new GetAccountTransactionsQuery(accountId));
        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return BadRequest(result.Error);
    }
    
    [HttpGet("{accountId}/{month}/{year}")]
    public async Task<IActionResult> ReportsAsync(Guid accountId, int month, int year, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMonthlyTransactionReportQuery(accountId,month,year), cancellationToken);
        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return BadRequest(result.Error);
    }
}