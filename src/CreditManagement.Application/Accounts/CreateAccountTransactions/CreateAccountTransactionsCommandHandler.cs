using System.Text.Json;
using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Accounts;
using CreditManagement.Domain.Primitives;
using CreditManagement.Domain.Primitives.Result;
using CreditManagement.Domain.Transactions;

namespace CreditManagement.Application.Accounts.CreateAccountTransactions;


public class CreateAccountTransactionsCommandHandler : ICommandHandler<CreateAccountTransactionsCommand, Result>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public CreateAccountTransactionsCommandHandler(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateAccountTransactionsCommand request, CancellationToken cancellationToken)
    {
        var lstAccountDtos = JsonSerializer.Deserialize<List<AccountRequestDto>>(request.AccountDetails, _jsonSerializerOptions);

        if (lstAccountDtos == null)
        {
            return Result.Failure(Error.Validation("Accounts.Invalid", "Invalid JSON data."));
        }

        foreach (var accountDto in lstAccountDtos)
        {
            var account = await _accountRepository.GetAccountByAccountNumberAsync(accountDto.AccountNumber);
            if (account is null)
            {
                // Account doesn't exist, create a new account with transactions.
                account = Account.Create(accountDto.AccountNumber, accountDto.AccountHolder, accountDto.Balance);
                AddTransactionsToAccount(account, accountDto.Transactions);
                _accountRepository.Insert(account);
            }
            else
            {
                // Account exists, update holder and balance, and add only new transactions.
                account.UpdateAccountHolder(accountDto.AccountHolder);
                account.UpdateBalance(accountDto.Balance);
                AddTransactionsToAccount(account, accountDto.Transactions);
                // foreach (var transactionDto in accountDto.Transactions)
                // {
                //     // if (account.Transactions.Any(t =>
                //     //         t.Date == transactionDto.Date && t.Amount == transactionDto.Amount &&
                //     //         t.Description == transactionDto.Description))
                //     // {
                //     //     continue;
                //     // }
                //
                //     var categorizedTransaction = Transaction.Create(transactionDto.Date, transactionDto.Amount, transactionDto.Description, account.Id);
                //     var resultIsAnomaly = account.IsAnomalyTransaction(transactionDto.Date, transactionDto.Amount, transactionDto.Description);
                //
                //     if (resultIsAnomaly.IsSuccess)
                //     {
                //         categorizedTransaction.SetAnomaly();
                //     }
                //
                //     account.AddTransaction(categorizedTransaction);
                //     _transactionRepository.Insert(categorizedTransaction);
                // }

                _accountRepository.Update(account);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    private void AddTransactionsToAccount(Account account, IEnumerable<TransactionRequestDto> transactions)
    {
        foreach (var transactionDto in transactions)
        {
            var transaction = Transaction.Create(transactionDto.Date, transactionDto.Amount, transactionDto.Description, account.Id);
            var resultIsAnomaly = account.IsAnomalyTransaction(transactionDto.Date, transactionDto.Amount, transactionDto.Description);

            if (resultIsAnomaly.IsSuccess)
            {
                transaction.SetAnomaly();
            }

            account.AddTransaction(transaction);
            _transactionRepository.Insert(transaction);
        }
    }
}