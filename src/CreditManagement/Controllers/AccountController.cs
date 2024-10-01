

using CreditManagement.Domain.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CreditManagement.Controllers;

public class AccountController(IAccountRepository accountRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var accounts = await accountRepository.GetAllAccountsWithTransactionsAsync();
        return View(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> UploadJsonFileAsync()
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var stream = new StreamReader(file.OpenReadStream());
        var jsonData = await stream.ReadToEndAsync();
        // var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonData);
        //
        // foreach (var account in accounts)
        // {
        //     if (accountRepository.GetAll().Any(a => a.AccountId == account.AccountId))
        //     {
        //         var existingAccount = await accountRepository.GetByIdAsync(account.AccountId);
        //         existingAccount.Transactions.AddRange(account.Transactions);
        //     }
        //     else
        //     {
        //         await accountRepository.AddAsync(account);
        //     }
        // }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ViewTransactionsAsync(Guid accountId)
    {
        var account = await accountRepository.GetAccountByIdWithTransactionsAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }

        return View(account.Transactions);
    }
}